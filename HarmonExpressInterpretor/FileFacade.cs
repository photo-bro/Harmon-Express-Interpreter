/*
 * HarmonExpressInterpreter
 * FileFacade
 * 
 * Author: Josh Harmon
 * Created: 3/21/13
 * Last Modified: 4/4/13
 * 
 * Description:
 * Provide simplified interface for opening and loading
 * files with data validation.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HarmonExpressInterpretor
{
    class FileFacade
    {
        // Class data
        StreamReader m_File;
        string m_sFilePath;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileFacade()
        { /* Do nothing */ }

        /// <summary>
        /// Pre: none
        /// Post: Class file has been intialized with chosen file and path.
        /// </summary>
        public bool OpenFile()
        {
            // Get file from user
            OpenFileDialog openForm = new OpenFileDialog();
            openForm.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openForm.ShowDialog() == DialogResult.OK)
            {
                m_sFilePath = openForm.FileName;
                m_File = new StreamReader(openForm.OpenFile());
                return true;
            }
            else
            {
                // User canceled window
                m_sFilePath = "";
                m_File = null;
                return false;
            }
        }


        /// <summary>
        /// Pre: none
        /// Post: Class file has been closed.
        /// </summary>
        public void CloseFile() 
        { m_File.Close(); }

        /// <summary>
        /// Pre: none
        /// Post: Class file path has been returned.
        /// </summary>
        public string getFilePath()
        { return m_sFilePath; }

        /// <summary>
        /// Pre: none
        /// Post: Class file has been returned.
        /// </summary>
        public StreamReader getFile 
        { get { return m_File; } }

        /// <summary>
        /// Pre:none
        /// Post: Contents in class file have been converted to string
        /// and returned. NOTE Position in file has been reset to beginning.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (m_File != null)
            {
                string sReturn = m_File.ReadToEnd();
                // Reset file pointer to beginning
                // Credit: stackoverflow.com/questions/6467853/return-streamreader-to-beginning-when-his-basestream-has-bom
                m_File.BaseStream.Position = 0;
                m_File = new StreamReader(m_File.BaseStream, m_File.CurrentEncoding, false);
                return sReturn;
            }
            return "";
        }
    }
}
