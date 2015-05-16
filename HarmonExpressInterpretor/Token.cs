/*
 * HarmonExpressInterpreter
 * Token
 * 
 * Author: Josh Harmon
 * Created: 3/21/13
 * Last Modified: 3/25/13
 * 
 * Description:
 * Defines the operations and data for a token
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarmonExpressInterpretor
{
    class Token
    {
        //Class Data
        public enum TokenType { NULL = 0, DOUBLE, LPAREN, RPAREN, OPERATOR, IDENTIFIER };
        private string m_sName;
        private double m_dValue;
        private TokenType m_TokenType;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Token()
        {
            m_sName = "";
            m_dValue = 0.0;
            m_TokenType = TokenType.NULL;
        }

        /// <summary>
        /// Pre: none
        /// Post: Class variables have been set to argument values.
        /// </summary>
        public Token(string sName, double dValue, TokenType Type)
        {
            m_sName = sName;
            m_dValue = dValue;
            m_TokenType = Type;
        }

        /// <summary>
        /// Pre: none
        /// Post: Token name has been returned.
        /// </summary>
        public string Name { get { return m_sName; } }

        /// <summary>
        /// Pre: none
        /// Post: Token value has been returned.
        /// </summary>
        public double Value { get { return m_dValue; } }

        /// <summary>
        /// Pre: none
        /// Post: Token type has been returned.
        /// </summary>
        public TokenType Type { get { return m_TokenType; } }

        /// <summary>
        /// Pre: none
        /// Post: String containing the name, value, and type of token has
        /// been returned.
        /// </summary>
        public override string ToString()
        {
            //string sName, sValue, sType;
            // Format string
            //sName = m_sName;
            //sValue = m_dValue.ToString();
            //sValue = sValue.PadLeft(25 - m_sName.Length*2, ' ');
            //sType = TypeToString(m_TokenType);
            //sType =  sType.PadLeft(25 - m_dValue.ToString().Length, ' ');

            //return sName + sValue + sType;
            return string.Format("{0,-15}{1,-15}{2,-15}", m_sName, m_dValue, TypeToString(m_TokenType));
        }

        /// <summary>
        /// Pre: none
        /// Post: String containing the type of TokenType type has
        /// been returned.
        /// </summary>
        private string TypeToString(TokenType type)
        {
            string sTemp;
            switch (type)
            {
                case TokenType.NULL:
                    sTemp = "NULL";
                    break;
                case TokenType.DOUBLE:
                    sTemp = "DOUBLE";
                    break;
                case TokenType.LPAREN:
                    sTemp = "LPAREN";
                    break;
                case TokenType.RPAREN:
                    sTemp = "RPAREN";
                    break;
                case TokenType.OPERATOR:
                    sTemp = "OPERATOR";
                    break;
                case TokenType.IDENTIFIER:
                    sTemp = "IDENTIFIER";
                    break;
                default:
                    sTemp = "";
                    break;
            }

            return sTemp;
        }
    }
}
