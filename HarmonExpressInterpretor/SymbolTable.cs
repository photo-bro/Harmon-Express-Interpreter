/*
 * HarmonExpressInterpretor
 * SymbolTable
 * 
 * Author: Josh Harmon
 * Created: 3/26/13
 * Last Modified: 4/2/13
 * 
 * Description:
 * Wrapper class for the Collections.Generic Dictionary class
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarmonExpressInterpretor
{
    class SymbolTable
    {
        // Class data
        private Dictionary<string, double> m_dicSymbolTable;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SymbolTable()
        { m_dicSymbolTable = new Dictionary<string, double>();}

        /// <summary>
        /// Post: Definition has been added to symbol table. If sId exists in table
        ///  previous value has been overwritten.
        /// </summary>
        public void addSymbol(string sId, double dValue)
        {
            try { m_dicSymbolTable.Add(sId, dValue); }
            catch (Exception e)
            {/* Do nothing */}
        }

        /// <summary>
        /// Post: Value for sId has been returned if it exists in table. Else
        ///  0.0 has been returned.
        /// </summary>
        public double findSymbol(string sId)
        {
            double dTemp = 0.0;
            if (m_dicSymbolTable.ContainsKey(sId))
                dTemp = m_dicSymbolTable[sId];
            return dTemp;
        }
    }
}
