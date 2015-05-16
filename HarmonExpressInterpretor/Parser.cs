/*
 * HarmonExpressInterpreter
 * Parser
 * 
 * Author: Josh Harmon
 * Created: 3/21/13
 * Last Modified: 4/3/13
 * 
 * Description:
 *  Contains the methods to parse a given list of tokens given
 * a specified grammar.
 * 
 * Grammar:
 * Expression = Term RestExpression
 * RestExpression = null | + Term RestExpression | - Term RestExpression
 * Term = Factor RestTerm
 * RestTerm = null | * Factor RestTerm | / Factor RestTerm | % Factor RestTerm | ^ Factor RestTerm
 * Factor = Double | Identifier | ( Expression ) | -Double
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XArray;

namespace HarmonExpressInterpretor
{
    class ParserOld
    {
        // Class data
        private XArray<Token> m_xaTokenList;
        private SymbolTable m_SymbolTable;
        private int m_iTP; // Token Pointer

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ParserOld()
        {
            m_xaTokenList = new XArray<Token>();
            m_SymbolTable = new SymbolTable();
            m_iTP = 0;
        }

        /// <summary>
        /// Pre: none
        /// Post: Parser object has been created with tokenlist
        /// </summary>
        public ParserOld(XArray<Token> xaTokenList)
        {
            m_xaTokenList = new XArray<Token>();
            // Deep copy
            foreach (Token t in xaTokenList)
                m_xaTokenList.Add(new Token(t.Name, t.Value, t.Type));
            m_SymbolTable = new SymbolTable();
            m_iTP = 0;
        }

        /// <summary>
        /// Pre: none
        /// Post: A parent node for a node tree containing the parsed
        /// token list has been returned. 
        /// </summary>
        public Node ParseExpression()
        {
            m_iTP = 0;
            // Check for empty expression
            if (m_xaTokenList[m_iTP] == null) return new NullNode(null, null);
            // Check if assignment
            else if (m_xaTokenList[m_iTP].Type == Token.TokenType.IDENTIFIER &&
                m_xaTokenList[m_iTP + 1] != null &&
                m_xaTokenList[m_iTP + 1].Name[0] == '=')
                return Assignment();
            else // Expression
                return Expression();
        }

        /// <summary>
        /// Pre: none
        /// Post: Head node for parsed expression has been returned. If
        ///  parse error has occured Head node = NullNode and error form
        ///  has been displayed
        /// </summary>
        public Node ParseExpression(XArray<Token> xaTokenList)
        {
            m_xaTokenList = new XArray<Token>();
            // Deep copy tokens
            foreach (Token t in xaTokenList)
                m_xaTokenList.Add(new Token(t.Name, t.Value, t.Type));
            return ParseExpression(); // Evaluate expression
        }
        
        /// <summary>
        /// See grammar in class description
        /// </summary>
        private Node Assignment()
        {
            Node tempNode;
            // Get ID name
            string sID = m_xaTokenList[m_iTP].Name;
            m_iTP += 2;   // erase 2 tokens
            if (m_xaTokenList[m_iTP] != null)
            {
                tempNode = Expression(); // Evaluate right side
                m_SymbolTable.addSymbol(sID, tempNode.Value); // Add value to symbol table
            }
            // right side is empty
            else
            {
                ErrorBox("Invalid Assignment\r\nExpecting Expression or Value\r\n",
                                "Invalid Assignment");
                tempNode = new NullNode(null, null);
            }

            return tempNode;
        }

        /// <summary>
        /// See grammar in class description
        /// </summary>
        private Node Expression()
        { return RestExp(Term()); }

        /// <summary>
        /// See grammar in class description
        /// Post: AddNode or Subtract Node has been returned.
        /// </summary>
        private Node RestExp(Node inNode)
        {
            // Check if null
            if (m_xaTokenList[m_iTP] == null) return inNode;
            // Check if current token is +|-
            else if (m_xaTokenList[m_iTP].Name[0] == '+' ||
                m_xaTokenList[m_iTP].Name[0] == '-')
            {
                // Token devoured
                if (m_xaTokenList[m_iTP++].Name[0] == '-') return RestExp(new SubtractNode(inNode, Term()));
                else return RestExp(new AddNode(inNode, Term()));
            }
            // Pass token through
            else return inNode;
        }

        /// <summary>
        /// See grammar in class description
        /// </summary>
        private Node Term()
        { return RestTerm(Factor()); }

        /// <summary>
        /// See grammar in class description
        /// Post: MulNode, DivNode, ModNode, or ExpNode has been returned.
        /// </summary>
        private Node RestTerm(Node inNode)
        {
            // Check if null
            if (m_xaTokenList[m_iTP] == null) return inNode;
            // Check if current token is *|\|%|^
            else if (m_xaTokenList[m_iTP].Name[0] == '*' ||
                m_xaTokenList[m_iTP].Name[0] == '/' ||
                m_xaTokenList[m_iTP].Name[0] == '%' ||
                m_xaTokenList[m_iTP].Name[0] == '^')
            {
                // Token devoured
                switch (m_xaTokenList[m_iTP++].Name[0])
                {
                    case '*': return RestTerm(new MulNode(inNode, Factor()));
                    case '/': return RestTerm(new DivNode(inNode, Factor()));
                    case '%': return RestTerm(new ModNode(inNode, Factor()));
                    case '^': return RestTerm(new ExpNode(inNode, Factor()));
                    default:
                        ErrorBox(string.Format("Invalid Operator\r\nExpecting *, /, &, or ^\r\nToken:\r\n{0}", m_xaTokenList[m_iTP].ToString()),
                            "Invalid Operator");
                        return new NullNode(null, null);
                    //error
                }
            }
            else return inNode; // Pass node through        
        }

        /// <summary>
        /// See grammar in class description
        /// Post: DoubleNode or IDNode have been returned.
        /// </summary>
        private Node Factor()
        {
            // Return null node if token is null
            if (m_xaTokenList[m_iTP] == null) return new NullNode(null, null);
            switch (m_xaTokenList[m_iTP].Type)
            {
                // Double
                case Token.TokenType.DOUBLE:
                    return new DoubleNode(null, null, m_xaTokenList[m_iTP++].Value); // 1 token destroyed
                // Identifier
                case Token.TokenType.IDENTIFIER:
                    ++m_iTP; // 1 token annihilated
                    // Check symbol table for ID value
                    return new IDNode(null, null, /* ID name */ m_xaTokenList[m_iTP - 1].Name,
                        /* Associated Value */ m_SymbolTable.findSymbol(m_xaTokenList[m_iTP - 1].Name));
                // Parenthesis's
                case Token.TokenType.LPAREN:
                    ParenNode tempNode;
                    ++m_iTP; // extirpate left paren
                    tempNode = new ParenNode(Expression(), null);
                    ++m_iTP; // erradicate right paren
                    return tempNode;
                // Negative Doubles
                case Token.TokenType.OPERATOR:
                    // Check if unary '-'
                    if (m_xaTokenList[m_iTP].Name[0] == '-')
                    {
                        // Check if next token is exists
                        if (m_xaTokenList[m_iTP + 1] == null)
                        {
                            ErrorBox(string.Format("Expecting Double Token\r\nToken:\r\n{0}", m_xaTokenList[m_iTP].ToString()),
                                "Expecting Double Token");
                            return new NullNode(null, null);
                        }
                        else
                        {
                            ++m_iTP;      // Eat 1 token
                            int iCount = 0;         // Count tokens consumed
                            // Eat excessive unary '-'s
                            while (m_xaTokenList[m_iTP].Name[0] == '-')
                            {
                                ++m_iTP;  // Eat token
                                ++iCount;
                                if (m_xaTokenList[m_iTP] == null)
                                {
                                    ErrorBox("Next Token not found\r\nExpecting Double Token", "Next Token not found");
                                    return new NullNode(null, null);
                                }
                            }
                            // Create node
                            if (m_xaTokenList[m_iTP].Type == Token.TokenType.DOUBLE)
                            {
                                // iCount is even then negative
                                ++m_iTP; // Eat token
                                if (iCount % 2 == 0)
                                    return new DoubleNode(null, null, -m_xaTokenList[m_iTP - 1].Value); // Negative
                                else
                                    return new DoubleNode(null, null, m_xaTokenList[m_iTP - 1].Value); // Positive
                            }
                            else // Error
                            {
                                ErrorBox(string.Format("Invalid Token\r\nExpecting Double Token\r\nToken:\r\n{0}", m_xaTokenList[m_iTP].ToString()),
                                "Invalid Token");
                                return new NullNode(null, null);
                                // error
                            }
                        }
                    }
                    else
                    {
                        // ERROR invalid Factor
                        ErrorBox(string.Format("Invalid Unary Operator\r\nExpecting - Operator\r\nToken:\r\n{0}", m_xaTokenList[m_iTP].ToString()),
                            "Invalid Unary Operator");
                        return new NullNode(null, null);
                    }
                default:
                    // ERROR not a factor error
                    ErrorBox(string.Format("Invalid Factor\r\nMust be double or identifier\r\nToken:\r\n{0}", m_xaTokenList[m_iTP].ToString()),
                "Invalid Factor");
                    return new NullNode(null, null);
            }
        }

        /// <summary>
        /// Pre: none
        /// Post: Error form with description sErrType and caption sErrCaption
        /// has been displayed.
        /// </summary>
        private void ErrorBox(string sErrType, string sErrCaption)
        {
            MessageBox.Show(string.Format("Error: {0}", sErrType), sErrCaption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}