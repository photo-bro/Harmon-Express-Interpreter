/*
 * HarmonExpressInterpreter
 * Node
 * 
 * Author: Josh Harmon
 * Created: 3/28/13
 * Last Modified: 4/3/13
 * 
 * Description:
 *  Contains node classes and methods for
 * the following operations:
 * +, -, *, /, %, ^, Double, Identifier, Null, ()
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarmonExpressInterpretor
{
    public abstract class Node
    {
        // Class data
        public enum NodeType { Null = 0, AddNode, SubtractNode, MulNode, DivNode, ModNode, ExpNode, IDNode, ParenNode, DoubleNode };
        protected Node m_leftNode, m_rightNode;
        protected NodeType m_nType;
        protected double m_dValue;

        /// <summary>
        /// Default inherited constructor
        /// </summary>
        protected Node(Node leftNode, Node rightNode)
        {
            m_leftNode = leftNode;
            m_rightNode = rightNode;
        }

        /// <summary>
        /// Post: Node value has been set or returned
        /// </summary>
        public virtual double Value
        {
            get { return m_dValue; }
            set { m_dValue = value; }
        }

        /// <summary>
        /// Post: Node type has been returned.
        /// </summary>
        public virtual NodeType Type
        { get { return NodeType.Null; } }

        /// <summary>
        /// Post: String representation of Node has been returned
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        { return string.Format("{0}     {1}\r\n", m_nType.ToString(), Value); }

        /// <summary>
        /// Recursive print method for printing node tree
        /// Post: Current node and child nodes have been concatenated and returned 
        /// as a string.
        /// </summary>
        public virtual string ToString(string sSpace)
        {
            string sTemp = "";

            // Append node information
            sTemp += string.Format("{0}{1}     {2}\r\n", sSpace, m_nType.ToString(), Value);
            // Append left child information
            if (m_leftNode != null)
                sTemp += string.Format("{0}{1}", sSpace, m_leftNode.ToString(sSpace + "  "));
            // Append right child information
            if (m_rightNode != null)
                sTemp += string.Format("{0}{1}", sSpace, m_rightNode.ToString(sSpace + "  "));

            return sTemp;
        }
    }

    public class NullNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// </summary>
        public NullNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        { m_nType = NodeType.Null; }

        /// <summary>
        /// See parent class
        /// </summary>
        public override double Value
        {
            get
            { return 0.0; }
            set
            { base.Value = value; }
        }

        /// <summary>
        ///  See parent class
        /// </summary>
        public override NodeType Type
        { get { return NodeType.Null; } }
    }

    public class AddNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// </summary>
        public AddNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        { m_nType = NodeType.AddNode; }

        /// <summary>
        /// Post: Value has been set to the sum of child nodes
        /// </summary>
        public override double Value
        {
            get
            { return m_leftNode.Value + m_rightNode.Value; }
            set
            { base.Value = value; }
        }

        /// <summary>
        ///  See parent class
        /// </summary>
        public override NodeType Type
        { get { return NodeType.AddNode; } }
    }

    public class SubtractNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// </summary>
        public SubtractNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        { m_nType = NodeType.SubtractNode; }

        /// <summary>
        /// Post: Value has been set to the difference of child nodes
        /// </summary>
        public override double Value
        {
            get
            { return m_leftNode.Value - m_rightNode.Value; }
            set
            { base.Value = value; }
        }

        public override NodeType Type
        { get { return NodeType.SubtractNode; } }
    }

    public class MulNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// </summary>
        public MulNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        { m_nType = NodeType.MulNode; }

        /// <summary>
        /// Post: Value has been set to leftNode * rightNode
        /// </summary>
        public override double Value
        {
            get
            { return m_leftNode.Value * m_rightNode.Value; }
            set
            { base.Value = value; }
        }

        /// <summary>
        /// See parent class
        /// </summary>
        public override NodeType Type
        { get { return NodeType.MulNode; } }
    }

    public class DivNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// </summary>
        public DivNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        { m_nType = NodeType.DivNode; }

        /// <summary>
        /// Post: Value has been set to leftNode / RightNode
        /// </summary>
        public override double Value
        {
            get
            { return m_leftNode.Value / m_rightNode.Value; }
            set
            { base.Value = value; }
        }

        /// <summary>
        /// See parent class
        /// </summary>
        public override NodeType Type
        { get { return NodeType.DivNode; } }
    }

    public class ModNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// </summary>
        public ModNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        { m_nType = NodeType.ModNode; }

        /// <summary>
        /// Post: Value has been set to rightNode % leftNode
        /// </summary>
        public override double Value
        {
            get
            { return m_leftNode.Value % m_rightNode.Value; }
            set
            { base.Value = value; }
        }

        /// <summary>
        /// See parent class
        /// </summary>
        public override NodeType Type
        { get { return NodeType.ModNode; } }
    }

    public class ExpNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// </summary>
        public ExpNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        { m_nType = NodeType.ExpNode; }

        /// <summary>
        /// Post: Value has been set to rightnode ^ leftNode
        /// </summary>
        public override double Value
        {
            get
            { return Math.Pow(m_leftNode.Value, m_rightNode.Value); }
            set
            { base.Value = value; }
        }

        public override NodeType Type
        { get { return NodeType.ExpNode; } }
    }

    public class IDNode : Node
    {
        // Class data
        string m_sID;

        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// - ID string has been set
        /// - Value has been set
        /// </summary>
        public IDNode(Node leftNode, Node rightNode, string sID, double dValue)
            : base(leftNode, rightNode)
        {
            m_nType = NodeType.IDNode;
            m_sID = sID;
            m_dValue = dValue;
        }

        /// <summary>
        /// See parent class
        /// </summary>
        public override double Value
        {
            get
            { return m_dValue; }
            set
            { base.Value = value; }
        }

        /// <summary>
        /// See parent class
        /// </summary>
        public override NodeType Type
        { get { return NodeType.IDNode; } }

        /// <summary>
        /// Post: Node has been represented as string as:
        /// sSpaceTYPE   ID    VALUE
        /// </summary>
        public override string ToString(string sSpace)
        {
            return string.Format("{0}{1}  {2}   {3}\r\n", sSpace, m_nType.ToString(), m_sID, Value);
        }
    }

    public class DoubleNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// - Value has been set
        /// </summary>
        public DoubleNode(Node leftNode, Node rightNode, double dValue)
            : base(leftNode, rightNode)
        {
            m_nType = NodeType.DoubleNode;
            m_dValue = dValue;
        }

        /// <summary>
        /// See parent class
        /// </summary>
        public override double Value
        {
            get
            { return m_dValue; }
            set
            { base.Value = value; }
        }

        /// <summary>
        /// See parent class
        /// </summary>
        public override NodeType Type
        { get { return NodeType.DoubleNode; } }
    }

    public class ParenNode : Node
    {
        /// <summary>
        /// Constructor - inherits parent constructor
        /// Appended:
        /// - NodeType has been set
        /// </summary>
        public ParenNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        { m_nType = NodeType.ParenNode; }

        /// <summary>
        /// Post: Value of leftNode has been returned
        /// </summary>
        public override double Value
        {
            get
            { return m_leftNode.Value; }
            set
            { base.Value = value; }
        }

        /// <summary>
        /// See parent class
        /// </summary>
        public override NodeType Type
        { get { return NodeType.ParenNode; } }
    }
}