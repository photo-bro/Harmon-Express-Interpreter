/*
 * HarmonExpressInterpretor
 * ParserOld
 * NOT WORKING
 * 
 * Author: Josh Harmon
 * Created: 3/21/13
 * Last Modified:
 * 
 * Description:
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XArray;

namespace HarmonExpressInterpretor
{
    class ParserOld
    {
        // Class data
        private XArray<Token> m_xaTokenList;
        private SymbolTable m_SymbolTable;
        private Token m_curToken;
        private int m_iTokenPointer;
        private ParenNode m_parNode;
        private ErrorHandler m_errHandler;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ParserOld(XArray<Token> xaTokenList)
        {
            m_xaTokenList = new XArray<Token>();
            // Deep copy
            foreach (Token t in xaTokenList)
                m_xaTokenList.Add(new Token(t.Name, t.Value, t.Type));

            m_SymbolTable = new SymbolTable();
            m_curToken = new Token();
            m_iTokenPointer = 0;
            m_errHandler = new ErrorHandler();
        }

        /// <summary>
        /// Pre: none
        /// Post: A parent node for a node tree containing the parsed
        /// token list has been returned. 
        /// </summary>
        public Node ParseExpression()
        {
            if (isExpression(m_iTokenPointer))
                m_parNode = (ParenNode) Expression();            
            // Add statement and assignment here
            else
                m_parNode = null;
            return m_parNode;
        }

        /// <summary>
        /// Pre: none
        /// Post: An AddNode, SubtractNode, or ParenNode has been created with children nodes 
        /// and returned.
        /// </summary>
        public Node Expression()
        {
            Node nodeTemp = null;

            // Expression
            if (isExpression(m_iTokenPointer))
                nodeTemp = new ParenNode(Term(), null, double.NaN);
            // Term RestExp
            else if (isTerm(m_iTokenPointer) && isRestExp(m_iTokenPointer + 1))
                // Determine what type of node
                switch (m_xaTokenList[m_iTokenPointer].Name[0])
                {
                    case '+':
                        nodeTemp = new AddNode(Term(), RestExp(), double.NaN);
                        break;
                    case '-':
                        nodeTemp = new SubtractNode(Term(), RestExp(), double.NaN);
                        break;
                    default:
                        break;
                }
            return (Node) nodeTemp;
        }

        /// <summary>
        /// Pre: none
        /// Post: An AddNode, SubtractNode, or ParenNode has been created with children nodes 
        /// and returned.
        /// </summary>
        public Node RestExp()
        {
            Node nodeTemp = null;

            // +/- Term RestExp
            if (isOp(m_iTokenPointer) && isTerm(m_iTokenPointer+1) && isRestExp(m_iTokenPointer+2))
            {
                m_iTokenPointer++; // Consume operator token
                // Determine if operator is + or -
                switch (m_xaTokenList[m_iTokenPointer-1].Name[0])
                {     
                    case '+':
                        nodeTemp = new AddNode(Term(), RestExp(), double.NaN);
                        break;
                    case '-':
                        nodeTemp = new SubtractNode(Term(), RestExp(), double.NaN);
                        break;
                    default:
                        // Error
                        m_errHandler.ThrowError(ErrorHandler.Error.NotAnOperator);
                        break;
                }
            }

            return nodeTemp;
        }

        /// <summary>
        /// Pre: none
        /// Post: A DoubleNode has been created with children nodes 
        /// and returned.
        /// </summary>
        public Node Term()
        {
            Node nodeTemp = null;
             
            if (isFactor(m_iTokenPointer))
            {
                // Factor RestExp
                if (!isEmpty(m_iTokenPointer + 1) && isRestExp(m_iTokenPointer + 1))
                    nodeTemp = new DoubleNode(Factor(), RestExp(), double.NaN);
                else
                    nodeTemp = new DoubleNode(Factor(), null, double.NaN);
            }
            else
            {
                m_errHandler.ThrowError(ErrorHandler.Error.NotAFactor);
            }

            return nodeTemp;
        }

        /// <summary>
        /// Pre: none
        /// Post: An MulNode, DivNode, has been created with children nodes 
        /// and returned.
        /// </summary>
        public Node RestTerm()
        {
            Node nodeTemp = null;

            if (isOp(m_iTokenPointer) && isFactor(m_iTokenPointer + 1) && isRestExp(m_iTokenPointer+2))
            {
                // Determine if operator is * or /
                switch (m_xaTokenList[m_iTokenPointer].Name[0])
                {
                    case '*':
                        nodeTemp = new MulNode(Factor(), RestExp(), double.NaN);
                        break;
                    case '/':
                        nodeTemp = new DivNode(Factor(), RestExp(), double.NaN);
                        break;
                    default:
                        // serror
                        m_errHandler.ThrowError(ErrorHandler.Error.NotAnOperator);
                        break;
                }
            }
            return nodeTemp;
        }

        public Node Factor()
        {
            Node nodeTemp = null;

            switch (m_xaTokenList[m_iTokenPointer].Type)
            {
                case Token.TokenType.DOUBLE:
                    nodeTemp = new DoubleNode(null, null, m_xaTokenList[m_iTokenPointer].Value);
                    m_iTokenPointer++; // 1 token consumed
                    break;
                case Token.TokenType.IDENTIFIER: 
                    // Check symbol table for ID value
                    nodeTemp = new IDNode(null, null, m_SymbolTable.findSymbol(m_xaTokenList[m_iTokenPointer].Name));
                    m_iTokenPointer++; // 1 token consumed
                    break;
                case Token.TokenType.LPAREN:
                    Token tokTemp = m_xaTokenList[m_iTokenPointer];
                    XArray<Token> xaParenthExp = new XArray<Token>();
                    while (tokTemp.Type != Token.TokenType.RPAREN)
                    {
                        xaParenthExp.Add(tokTemp);
                        m_iTokenPointer++; // 1 token consumed
                    }
                    ParserOld parenParse = new ParserOld(xaParenthExp);
                    nodeTemp = parenParse.ParseExpression();
                    break;
                case Token.TokenType.OPERATOR:
                    if (m_xaTokenList[m_iTokenPointer].Name[0] == '-' && m_xaTokenList[m_iTokenPointer+1].Type == Token.TokenType.DOUBLE)
                    {
                        nodeTemp = new DoubleNode(null, null, -m_xaTokenList[m_iTokenPointer].Value);
                        m_iTokenPointer += 2; // 2 tokens consumed
                    }
                    break;
                default:
                    //error
                    break;
            }
            return nodeTemp;
        }

        private bool isExpression(int iPos)
        {
            // Term RestExp
            if (isTerm(iPos) && isRestExp(iPos + 1))
                return true;

            return false;
        }

        private bool isRestExp(int iPos)
        {
            // e
            if (isEmpty(iPos))
                return true;
            // +/- Term RestExp
            if (isOp(iPos) && isFactor(iPos + 1) && isRestTerm(iPos + 2))
                if (m_xaTokenList[iPos].Name[0] == '+' ||
                    m_xaTokenList[iPos].Name[0] == '-')
                    return true;

            return false;
        }

        private bool isTerm(int iPos)
        {
            // Factor RestTerm
            if (isFactor(iPos) && isRestTerm(iPos + 1))
                return true;

            return false;
        }

        private bool isRestTerm(int iPos)
        {
            // e
            if (isEmpty(iPos))
                return true;
            // *|/ Factor RestTerm
            if (isOp(iPos) && isFactor(iPos + 1) && isRestTerm(iPos + 2))
                if (m_xaTokenList[iPos].Name[0] == '+' ||
                    m_xaTokenList[iPos].Name[0] == '-')
                    return true;
            
            return false;
        }

        private bool isFactor(int iPos)
        {
            // e
            if (isEmpty(iPos))
                return true;
            // DOUBLE / IDENTIFIER
            if (m_xaTokenList[iPos].Type == Token.TokenType.DOUBLE
                || m_xaTokenList[iPos].Type == Token.TokenType.IDENTIFIER)
                return true;
            // - Factor
            if (m_xaTokenList[iPos].Name[0] == '-' && isFactor(iPos + 1))
                return true;
            // ( Exp ) NOTE: Only checks for right parenthesis, not for Exp inside
            if (m_xaTokenList[iPos].Type == Token.TokenType.LPAREN
                && isExpression(iPos + 1))
            {
                for (int i = iPos; isEmpty(i); ++i)
                {
                    if (m_xaTokenList[i].Type == Token.TokenType.RPAREN)
                        return true;
                }
            }
            return false;
        }

        private bool isEmpty(int iPos)
        {
            if (m_xaTokenList[iPos] == null || m_xaTokenList[iPos].Type == Token.TokenType.NULL)
                return true;

            return false;
        }

        private bool isOp(int iPos)
        {
            if (isEmpty(iPos))
                return false;
            if (m_xaTokenList[iPos].Type == Token.TokenType.OPERATOR)
                return true;

            return false;
        }


    }
}
