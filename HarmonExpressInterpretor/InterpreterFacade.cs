/*
 * HarmonExpressInterpreter
 * InterpretorFacade
 * 
 * Author: Josh Harmon
 * Created: 3/21/13
 * Last Modified: 4/3/13
 * 
 * Description:
 * Provide a simplified interface to access all functions of 
 * the Tokenizer, Parser, and Node tree.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XArray;

namespace HarmonExpressInterpretor
{
    class InterpreterFacade
    {
        // Class variables
        Tokenizer m_Tokenizer;
        ParserOld m_Parser;
        Node m_nodeTreeHead;
        XArray<Token> m_xaTokenList;
        string m_sTokenString;

        /// <summary>
        /// Default Constructor
        /// All class values have been initialized to defaults
        /// </summary>
        public InterpreterFacade()
        {
            m_Tokenizer = new Tokenizer();
            m_xaTokenList = new XArray<Token>();
            m_Parser = new ParserOld();
            m_sTokenString = "";
        }

        /// <summary>
        /// All class values have been initialized to defaults except
        /// the internal token string which has been set to sTokenString
        /// </summary>
        /// <param name="sTokenString"></param>
        public InterpreterFacade(string sTokenString)
        {
            // Remove whitespace
            string sTemp = sTokenString.Replace(" ", "");

            m_sTokenString = sTemp;
            m_Tokenizer = new Tokenizer(m_sTokenString);
            m_xaTokenList = new XArray<Token>(); 
        }

        /// <summary>
        /// Pre: sInString is an computable expression.
        /// Post: The expression has been tokenized and evaluated. The expression
        /// value has been returned.
        /// </summary>
        public string InterpretString(string sExpression)
        {
            // Check if empty expression
            string sBlankMessage = "Please enter a proper expression\r\n";
            if (sExpression.Length == 0) return sBlankMessage;

            // Tokenize
            BuildTokenList(sExpression);
            
            //Evaluate Tokens
            m_nodeTreeHead =  m_Parser.ParseExpression(m_xaTokenList);
            if (m_nodeTreeHead == null) return sBlankMessage;
            switch (m_nodeTreeHead.Type)
            {
                case Node.NodeType.Null: return sBlankMessage;
                // if IDNode return ToString
                //case Node.NodeType.IDNode: return string.Format(m_nodeTreeHead.ToString(""));
                default: return string.Format("{0}",m_nodeTreeHead.Value);
            }
        }

        /// <summary>
        /// Pre: inFile is a text file containing a series of expressions seperated
        ///  by new line characters
        /// Post: All expressions in inFile have been evaluated and concatenated
        /// into a string and returned.
        /// </summary>
        public string InterpretFile(StreamReader inFile)
        {           
            // Array of expression strings
            XArray<string> xasExpressions = new XArray<string>();
            string sExpression = inFile.ReadLine();

            // Remove comments and build array of expression strings
            while (sExpression != null || sExpression != "")
            {
                if (sExpression == null) break;
                if (sExpression != "" && sExpression[0] != '#' && sExpression[0] != ' ')
                    xasExpressions.Add(sExpression);
                sExpression = inFile.ReadLine();
            }

            // Build tokenlist array
            XArray<XArray<Token>> xaxaTokenLists = new XArray<XArray<Token>>();
            foreach (string s in xasExpressions)
                xaxaTokenLists.Add(BuildTokenListArray(s));

            // Evaluate expressions
            StringBuilder sSolution = new StringBuilder();
            foreach (XArray<Token> xat in xaxaTokenLists)
                sSolution.Append(string.Format("{0}\r\n", m_Parser.ParseExpression(xat).Value));

            return sSolution.ToString();
        }

        /// <summary>
        /// Pre: none
        /// Post: Class token list has been concatenated into
        /// a formatted string and returned.
        /// </summary>
        public string GetTokenList()
        {
            StringBuilder sbTokens = new StringBuilder();
            foreach (Token t in m_xaTokenList)
            {
                sbTokens.Append(t.ToString());
                sbTokens.Append("\r\n");
            }
            return sbTokens.ToString();
        }

        /// <summary>
        /// Pre: none
        /// Post: Concated string containing information for each node
        /// in last parsed expression has been returned.
        /// </summary>
        public string ParseTree()
        {
            if (m_nodeTreeHead != null) return m_nodeTreeHead.ToString("");
            else return "Expression has not been parsed";
        }

        /// <summary>
        /// Pre: none
        /// Post: sInString has been tokenized into m_xaTokenList
        /// </summary>
        private void BuildTokenList(string sInString)
        {
            // Remove whitespace
            string sTemp = sInString.Replace(" ", "");
            m_Tokenizer = new Tokenizer(sTemp);

            // Tokenize
            m_xaTokenList = new XArray<Token>();
            while (m_Tokenizer.isNextToken())
                m_xaTokenList.Add(m_Tokenizer.GetNextToken());

        }

        /// <summary>
        /// Pre: none
        /// Post: XArray[Token] containing tokens from sInString has been
        ///  returned.
        /// </summary>
        private XArray<Token> BuildTokenListArray(string sInString)
        {
            // Remove whitespace
            string sTemp = sInString.Replace(" ", "");
            m_Tokenizer = new Tokenizer(sTemp);

            // Tokenize
            XArray<Token> xaTokens = new XArray<Token>();
            while (m_Tokenizer.isNextToken())
                xaTokens.Add(m_Tokenizer.GetNextToken());

            return xaTokens;
        }
    }
}
