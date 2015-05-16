/*
 * HarmonExpressInterpreter
 * Tokenizer
 * 
 * Author: Josh Harmon
 * Created: 3/21/13
 * Last Modified: 4/3/13
 * 
 * Description:
 * Convert a given string to an array of tokens
 * 
 * Token Grammar:
 *  IDENTIFIER:
 *   - Must begin with letter
 *   - Can contain only letters or digits
 *  DOUBLE:
 *   - Can contain only digits or decimal point
 *   - Can contain only one decimal point
 *   - Cannot begin with a decimal point
 *  LPAREN:
 *   - Single character '('
 *  RPAREN:
 *   - Single character ')'
 *  OPERATOR:
 *   - Single character
 *   - Either '+', '-', '*', '/', '=', '%', '^'
 *  NULL:
 *   - Any other character or string that does not comply
 *    with the above grammar.
 */
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XArray;

namespace HarmonExpressInterpretor
{
    class Tokenizer
    {
        // Class data
        private Token m_curToken;
        private string m_sInString;
        private int m_iStringPos;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Tokenizer()
        {
            m_curToken = new Token();
            m_sInString = "";
            m_iStringPos = 0;
        }

        /// <summary>
        /// Pre: none
        /// Post: Tokenizer object has been constructed with class token string
        /// set to sInString.
        /// </summary>
        public Tokenizer(string sInString)
        {
            m_curToken = new Token();
            m_sInString = sInString;
            m_iStringPos = 0;
        }

        /// <summary>
        /// Pre: none
        /// Post: Next token in class expression string has been
        /// returned. Position in class string has been incremented.
        /// </summary>
        public Token GetNextToken()
        {
            // IDENTIFIER
            if (isLetter())
            {
                // Build identifier string
                StringBuilder sbTemp = new StringBuilder();
                do
                {
                    sbTemp.Append(m_sInString[m_iStringPos++]);
                    if (m_iStringPos == m_sInString.Length) break;
                } while ((isLetter() || isDigit()) && !isBlankOrEnd());

                m_curToken = new Token(sbTemp.ToString(), 0, Token.TokenType.IDENTIFIER);
            }
            // DOUBLE
            else if (isDigit())
            {
                // Build double with only one decimal
                StringBuilder sbTemp = new StringBuilder();
                bool bOneDecimal = false;
                do
                {
                    sbTemp.Append(m_sInString[m_iStringPos++]);
                    if (m_iStringPos == m_sInString.Length) break;
                    // Check for decimal point
                    if (m_sInString[m_iStringPos] == 0x2E && !bOneDecimal)
                    {
                        sbTemp.Append(m_sInString[m_iStringPos++]);
                        bOneDecimal = true;
                    }
                } while (isDigit() && !isBlankOrEnd());

                m_curToken = new Token(sbTemp.ToString(), parseDouble(sbTemp.ToString()), Token.TokenType.DOUBLE);
            }
            // OPERATOR / LPAREN / RPAREN / NULL
            else
            {
                switch (m_sInString[m_iStringPos])
                {
                    case '(':
                        m_curToken = new Token("(", Double.NaN, Token.TokenType.LPAREN);
                        break;
                    case ')':
                        m_curToken = new Token(")", Double.NaN, Token.TokenType.RPAREN);
                        break;
                    case '+':
                        m_curToken = new Token("+", Double.NaN, Token.TokenType.OPERATOR);
                        break;
                    case '-':
                        m_curToken = new Token("-", Double.NaN, Token.TokenType.OPERATOR);
                        break;
                    case '*':
                        m_curToken = new Token("*", Double.NaN, Token.TokenType.OPERATOR);
                        break;
                    case '/':
                        m_curToken = new Token("/", Double.NaN, Token.TokenType.OPERATOR);
                        break;
                    case '=':
                        m_curToken = new Token("=", Double.NaN, Token.TokenType.OPERATOR);
                        break;
                    case '%':
                        m_curToken = new Token("%", Double.NaN, Token.TokenType.OPERATOR);
                        break;
                    case '^':
                        m_curToken = new Token("^", Double.NaN, Token.TokenType.OPERATOR);
                        break;
                    default:
                        // Display error
                        ErrorBox(string.Format("Invalid char\r\nExpression: {0}\r\nPosition: {1}\r\nchar: '{2}'", 
                            m_sInString, m_iStringPos, m_sInString[m_iStringPos].ToString()), "Invalid char in expression");
                        // Return null token by default
                        m_curToken = new Token(m_sInString[m_iStringPos].ToString(), Double.NaN, Token.TokenType.NULL);
                        break;
                }
                m_iStringPos++;
            }
            return m_curToken;
        }

        /// <summary>
        /// Pre: none
        /// Post: Class token string has been set to sInString.Next token in class 
        /// expression string has been returned. Position in class string has been 
        /// incremented.
        /// </summary>
        public Token GetNextToken(string sInString)
        {
            m_sInString = sInString;
            return GetNextToken();
        }

        /// <summary>
        /// Pre: none
        /// Post: True has been returned if there is a token available
        /// in the expression, else false has been returned.
        /// </summary>
        /// <returns></returns>
        public bool isNextToken()
        {
            if (m_iStringPos >= m_sInString.Length) return false;
            return true;
        }

        /// <summary>
        /// Pre:  none
        /// Post: True has been returned if the character at m_sInString[m_iStringPos}
        /// is a numerical digit. Else false has been returned.
        /// </summary>
        private bool isDigit()
        {
            return Char.IsDigit(m_sInString[m_iStringPos]);
            /*
            if (m_sInString[m_iStringPos] > 0x2F && m_sInString[m_iStringPos] < 0x3A)
                return true;
            return false;
             * */
        }

        /// <summary>
        /// Pre:  none
        /// Post: True has been returned if the character at m_sInString[m_iStringPos}
        /// is a letter. Else false has been returned.
        /// </summary>
        private bool isLetter()
        {
            return Char.IsLetter(m_sInString[m_iStringPos]);
            /*
            string sTemp = m_sInString.ToUpper();
            if ((sTemp[m_iStringPos] > 0x40 && sTemp[m_iStringPos] < 0x5A)
                 || sTemp[m_iStringPos] == 0x5F)
                return true;
            return false;
             * */
        }

        /// <summary>
        /// Pre:  none
        /// Post: True has been returned if the character at m_sInString[m_iStringPos}
        /// is a blank space, ETX, or EOT. Else false has been returned.
        /// </summary>
        private bool isBlankOrEnd()
        {
            if (m_sInString[m_iStringPos] == 0x20 || m_sInString[m_iStringPos] == 0x3 || m_sInString[m_iStringPos] == 0x4)
                return true;
            return false;
        }

        /// <summary>
        /// Pre: none
        /// Post: A parsed double from sIn has been returned if sIn has the
        /// proper format. Else 0.0 has been returned.
        /// </summary>
        private double parseDouble(string sIn)
        {
            double dTemp = 0.0;
            try
            { dTemp = Double.Parse(sIn, NumberStyles.AllowDecimalPoint); }
            catch (Exception e)
            { dTemp = 0.0; }

            return dTemp;
        }

        private void ErrorBox(string sErrString, string sErrCaption)
        {
            MessageBox.Show(string.Format("Error: {0}", sErrString), string.Format("Error: {0}", sErrCaption),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

    }
}
