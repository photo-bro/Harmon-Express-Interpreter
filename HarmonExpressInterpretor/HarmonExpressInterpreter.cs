/*
 * HarmonExpressInterpreter
 * HarmonExpressInterpreter
 * 
 * Author: Josh Harmon
 * Created: 3/21/13
 * Last Modified: 4/3/13
 * 
 * Description:
 * Provide a GUI for the operations in the InterpreterFacade
 * class.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HarmonExpressInterpretor
{
    public partial class HarmonExpressInterpreter : Form
    {
        // Class data
        FileFacade m_ffFile;
        InterpreterFacade m_Interpretor;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public HarmonExpressInterpreter()
        {
            InitializeComponent();
            m_Interpretor = new InterpreterFacade();
        }

        /// <summary>
        /// Pre: none
        /// Post: File has been evaluated and solutions have been
        /// compared to answers in file. Solutions have been printed to
        /// console. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openTomFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Load file
            m_ffFile = new FileFacade();
            if (m_ffFile.OpenFile()) // Calls directory form
            {
                // Interpret and print result
                tb_Console.Visible = true;
                tb_Console.Text = InterpretTomFile();

                // Disable single expression only options
                btn_Tokenize.Visible = false;
                btn_PrintParseTree.Visible = false;
                // Reveal menu items
                toggleConsoleBoxToolStripMenuItem.Visible = true;
                clearConsoleToolStripMenuItem.Visible = true;
                displayTokenListToolStripMenuItem.Visible = false;
                displayParseTreeToolStripMenuItem.Visible = false;
            }
            else // File did not open
            { /* Do nothing */ }
        }

        /// <summary>
        /// Pre: none
        /// Post: File has been opened and interpreted with the results displayed
        /// in form.
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ffFile = new FileFacade();
            if (m_ffFile.OpenFile()) // Calls directory form
            {
                // Evaluate file
                tb_Console.Visible = true;
                tb_Console.Text = m_Interpretor.InterpretFile(m_ffFile.getFile);

                // Reveal buttons
                btn_Tokenize.Visible = false;
                btn_PrintParseTree.Visible = false;

                // Reveal menu items
                toggleConsoleBoxToolStripMenuItem.Visible = true;
                clearConsoleToolStripMenuItem.Visible = true;
                displayTokenListToolStripMenuItem.Visible = false;
                displayParseTreeToolStripMenuItem.Visible = false;
            }
            else // file did not open
            { }
        }

        /// <summary>
        /// Pre: none
        /// Post: Token list has been displayed in form.
        /// </summary>
        private void displayTokenListToolStripMenuItem_Click(object sender, EventArgs e)
        { tb_Console.Text = getTokenList(); }

        /// <summary>
        /// Pre: none
        /// Post: Parse tree has been displayed in form
        /// </summary>
        private void displayParseTreeToolStripMenuItem_Click(object sender, EventArgs e)
        { tb_Console.Text = m_Interpretor.ParseTree(); }

        /// <summary>
        /// Pre: none
        /// Post: Console box has been cleared
        /// </summary>
        private void clearConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        { tb_Console.Text = ""; }

        /// <summary>
        /// Pre: none
        /// Post: Console text box has been toggled between visible and hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggleConsoleBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tb_Console.Visible) tb_Console.Visible = false;
            else tb_Console.Visible = true;
        }

        /// <summary>
        /// File->Quit
        /// Quits program
        /// </summary>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        { this.Close(); }

        /// <summary>
        /// Pre: none
        /// Post: Expression has been interpretated and the result has been displayed in 
        /// form
        /// </summary>
        private void btn_Go_Click(object sender, EventArgs e)
        {
            tb_Console.Visible = true;
            tb_Console.Text = m_Interpretor.InterpretString(tb_Expression.Text);
            btn_Tokenize.Visible = true; // Display Tokenize button
            btn_PrintParseTree.Visible = true;

            // Reveal menu items
            toggleConsoleBoxToolStripMenuItem.Visible = true;
            clearConsoleToolStripMenuItem.Visible = true;
            displayParseTreeToolStripMenuItem.Visible = true;
            displayTokenListToolStripMenuItem.Visible = true;
        }

        /// <summary>
        /// Pre: none
        /// Post: Token list has been displayed in form.
        /// </summary>
        private void btn_Tokenize_Click(object sender, EventArgs e)
        { tb_Console.Text = getTokenList(); }

        /// <summary>
        /// Pre: none
        /// Post: Parse tree has been displayed in form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintParseTree_Click(object sender, EventArgs e)
        { tb_Console.Text = m_Interpretor.ParseTree(); }

        /// <summary>
        /// Pre: none
        /// Post: Token list and heading have been concatenated into a string and returned.
        /// </summary>
        /// <returns></returns>
        private string getTokenList()
        {
            StringBuilder sbTemp = new StringBuilder();
            sbTemp.Append(string.Format("Token List:\r\n{0,-15}{1,-15}{2,-15}\r\n", "Name", "Value", "Type"));
            sbTemp.Append(m_Interpretor.GetTokenList());
            return sbTemp.ToString();
        }

        /// <summary>
        /// Pre: m_ffFile has been instantiated
        /// Post: File has been evaluated and solutions have been
        /// compared to answers in file. If the solutions match the answers
        /// Correct: 'Expression' = Solution
        /// has been printed to returned. If they do not
        /// Incorrect: 'Expression' != Solution 'Expression' = Answer
        /// has been returned.
        /// </summary>
        private string InterpretTomFile()
        {
            // Get file contents
            StringReader srFile = new StringReader(m_ffFile.ToString());

            // Remove Comments
            string sTemp = srFile.ReadLine();
            string sFileNoComment = "";
            while (sTemp != null)
            {
                if (sTemp != "" && sTemp[0] != '#' && sTemp[0] != ' ')
                    sFileNoComment += sTemp + "\r\n";
                sTemp = srFile.ReadLine();
            }
            StringReader srFileNoComment = new StringReader(sFileNoComment);

            // Evaluate file
            string sSolutions = m_Interpretor.InterpretFile(m_ffFile.getFile);

            // Compare solved answers to correct answers
            StringReader srReader = new StringReader(sSolutions);       // Read each solution line
            StringBuilder sbOut = new StringBuilder();                  // Build console output
            string sSol = "";
            string sAns = "";
            // Prime Read
            sSol = srReader.ReadLine();
            sAns = srReader.ReadLine();
            while (sSol != null || sAns != null)
            {
                if (string.Compare(sSol, sAns) == 0)
                    sbOut.Append(string.Format("Correct: '{0}' = {1}\r\n", srFileNoComment.ReadLine(), sSol));
                else
                    sbOut.Append(string.Format("Incorrect: '{0}' != {1}  '{0}' = {2}\r\n", srFileNoComment.ReadLine(), sSol, sAns));
                srFileNoComment.ReadLine(); // Skip line
                sSol = srReader.ReadLine(); // Get next solution   
                sAns = srReader.ReadLine(); // Get next answer
            }

            return sbOut.ToString();
        }

        /// <summary>
        /// Post: Information for the program has been printed to console box.
        /// </summary>
        private void aboutInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Display console box
            tb_Console.Visible = true;
            // Clear console
            tb_Console.Text = "";
            // Print information to console
            tb_Console.AppendText("Harmon Express Interpreter\r\nCSCI240 Professor: Dr. Tom Fuller\r\nBy Josh Harmon\r\nLast Modified 4/5/13\r\nVersion 1.0.0\r\n");
            tb_Console.AppendText("\r\nEnter an expression into the the text box above and press 'Go' to evaluate it.\r\n\r\nOpen TomFile evaluates expressions in a text file and compares the result with the next line.\r\n");
            tb_Console.AppendText("\r\nOpen Expression File evaluates expressions seperated by new lines in a text file and prints result to console.\r\n");
            tb_Console.AppendText("\r\nToken list and Parse tree can only be displayed when an expression has been evaluated through the text box above.\r\n");

        }
    }
}
