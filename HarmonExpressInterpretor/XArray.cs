/* *
 * XArray generic class
 * 
 * Author: Josh Harmon
 * Created: 2/5/13
 * Last Modified: 2/22/13
 * 
 * Description:
 *      XArray is a simple generic class that expands
 *      on the basic array. While traditional arrays
 *      cannot expand past their defined bounds, XArray
 *      can. XArray objects are dynamic arrays. The array
 *      doubles in size everytime it becomes full
 * 
 * Test Cases: 
 *      Tested for int, float, double, char, and string types.
 *      
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XArray
{
    /// <summary>
    /// XArray is a extendable dynamic generic array. When the array becomes full
    /// it array automatically expands to double its size to accomadate additional
    /// items. Default array size is 4. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XArray<T> : IEnumerable<T>
    {
        // Default size
        private const int DEFAULT_MAX = 10;
        // Class variables
        private int m_iCount, m_iPos, m_iMaxSize;
        private T[] m_aT;

        /// <summary>
        /// Default Constructor
        /// Pre: none
        /// Post: XArray object has been created with size DEFAULT_MAX.
        /// </summary>
        public XArray()
        {
            m_iCount = m_iPos = 0;
            m_iMaxSize = DEFAULT_MAX;
            // Create type array
            m_aT = new T[m_iMaxSize];
        }

        /// <summary>
        /// Pre: iGrowSize is an integer.
        /// Post: XArray object has been created with size iMaxSize.
        /// </summary>
        public XArray(int iMaxSize)
        {
            m_iCount = m_iPos = 0;
            m_iMaxSize = iMaxSize;
            // Create type array
            m_aT = new T[m_iMaxSize];
        }

        /// <summary>
        /// Pre: xaT is an XArray object.
        /// Post: XArray object has been created identical to xaT.
        ///  Size of XArray has been set to the size of xaT
        ///  </summary>
        public XArray(XArray<T> xaT)
        {
            m_iCount = m_iPos = 0;
            m_iMaxSize = xaT.Size;

            // Create type array
            m_aT = new T[m_iMaxSize];

            // Add in values from xaT
            m_iCount = m_iPos = xaT.Size;
            for (int i = 0; i < xaT.Size; i++)
                m_aT[i] = xaT[i];
        }

        /// <summary>
        /// Pre: none
        /// Post: Current number of XArray elements has been returned.
        /// </summary>
        public int Size
        { get { return m_iCount; } }

        /// <summary>
        /// Pre: none
        /// Post: Current position in XArray has been returned
        /// </summary>
        public int Position
        { get { return m_iPos; } }

        /// <summary>
        /// Pre: none
        /// Post: Current XArray position has been set to iPos
        /// </summary>
        public void setPosition(int iPos)
        { m_iPos = iPos; }

        /// <summary>
        /// Pre: T_item has been instantiated
        /// Post: T_item has been added to XArray above the last stored index. XArray size 
        ///  has been doubled if XArray object size was too small.
        ///  </summary>
        public void Add(T T_item)
        {
            // Check for array overflow
            if (m_iCount >= m_iMaxSize) Grow();
            // Add item 
            m_aT[m_iPos++] = T_item;
            m_iCount++;
        }

        /// <summary>
        /// Pre: xaT has been instantiated
        /// Post: All values in xaT have been deep copied into current
        ///  XArray object. 
        ///  </summary>
        public void Copy(XArray<T> xaT)
        {
            // Get blank XArray
            m_aT = new T[xaT.Size];

            // Copy items
            for (int i = 0; i < xaT.Size; i++)
                m_aT[i] = xaT[i];

            // Adjust count, default m_iMaxSize to item
            //  count of array copied
            m_iMaxSize = m_iCount = m_iPos = xaT.m_iCount;
        }

        /// <summary>
        /// Pre: none
        /// Post: All values in current XArray object have been set to
        ///  default type values. m_iCount has been reset.
        ///  </summary>
        public void RemoveAll()
        {
            // Check if there are items
            if (m_iCount > 0)
            {
                // Remove loop
                for (int i = m_iCount; i < m_iCount; i++)
                    m_aT[i] = default(T);

                // Reset count and position
                m_iCount = m_iPos = 0;
            }
        }

        /// <summary>
        /// Pre: iIndex is an integer.
        /// Post: Array access has been overrided. XArray objects are accessable
        ///  like regular arrays. (ex. arr[i] = item; item = arr[i])
        ///  </summary>
        public T this[int iIndex]
        {
            set
            {
                // Check for array overflow
                if (iIndex >= m_iMaxSize) Grow();
                // Add item
                m_iPos = iIndex;
                if (iIndex >= m_iCount)
                {
                    m_iCount++;
                    m_aT[m_iPos++] = value;
                }
            }
            get
            {
                // Out of bounds
                if (iIndex > m_iCount)
                {
                    return default(T);
                }
                try // just in case
                {
                    return m_aT[iIndex];
                }
                catch (Exception e) // if out of bounds
                {
                    // Throw exception here if design needs it
                    return default(T);
                }
            }
        }

        /// <summary>
        /// Pre: m_aT[] has been instantiated
        /// Post: m_aT has been increased by 10 and contains the same contents
        ///  as before at the same indexes.
        /// </summary>
        private void Grow()
        {
            // Create bigger array
            m_iMaxSize += 10;
            T[] aT_new = new T[m_iMaxSize];

            // Move items
            for (int i = 0; i < m_iCount; ++i)
                aT_new[i] = m_aT[i];
            m_aT = aT_new; // Point class array to bigger array
        }

        /// <summary>
        /// For IEnumerable<T> interface
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        { return new XArrayEnumerator<T>(this); }

        /// <summary>
        /// For IEnumerable interface
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return this.GetEnumerator(); }
    }


    /// <summary>
    /// XArrayEnumerator is an implentation of the XArray class with the IEnumrable
    ///  interface.
    /// Used Tom Fuller's BackFull source as example for IEnumerator/IEnumerable
    /// implementation example.
    /// </summary>
    public class XArrayEnumerator<T> : IEnumerator<T>
    {
        private XArray<T> m_xaT; // Current XArray object
        private int m_iPos;

        /// <summary>
        /// Pre: none
        /// Post: XArrayEnumerator object constructed from items in
        /// xaTIn
        /// </summary>
        public XArrayEnumerator(XArray<T> xaTIn)
        {
			// Default position
			m_iPos = -1;
            // Point interal XArray to argument
            m_xaT = xaTIn;
            // Deep copy
            for (int i = 0; i < xaTIn.Size; ++i)
                m_xaT[i] = xaTIn[i];
        }

        /// <summary>
        /// Pre: none
        /// Post: XArrayEnumerator has been reset to position -1
        /// </summary>
        public void Reset()
        { m_iPos = -1; }

        /// <summary>
        /// Pre: none
        /// Post: Next item has been set to current. True has been returned 
        ///  if next object is inside bounds. False has been returned if next
        ///  object is outside of bounds.
        /// </summary>
        public bool MoveNext()
        {
            m_iPos++;
            return (m_iPos < m_xaT.Size);
        }
        /// <summary>
        /// Pre: none
        /// Post: Current object has been returned.
        /// </summary>
        T IEnumerator<T>.Current
        { get { return (T)Current; } }

        /// <summary>
        /// Pre: none
        /// Post: Current object has been returned if inside bounds. Default value
        ///  has been returned if outside bounds.
        /// </summary>
        public object Current
        {
            get
            {
                if (m_iPos < 0) return default(T);
                else
                {
                    try
                    {
                        return m_xaT[m_iPos];
                    }
                    catch (Exception e)
                    {
                        // Throw exception here if design needs it
                        return default(T);
                    }
                }
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public void Dispose()
        {
            // Add implementation if needed
        }
	}
	
}

