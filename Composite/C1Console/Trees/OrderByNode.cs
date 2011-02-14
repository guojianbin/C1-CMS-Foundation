﻿using System.Linq.Expressions;


namespace Composite.C1Console.Trees
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public abstract class OrderByNode
    {
        /// <exclude />
        public string XPath { get; internal set; }

        /// <exclude />
        public DataElementsTreeNode OwnerNode { get; internal set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceExpression"></param>
        /// <param name="parameterExpression"></param>
        /// <param name="first">
        /// Implementations of this clase should use this parameter to distinguish between OrderBy and ThenBy
        /// </param>
        /// <returns></returns>
        public abstract Expression CreateOrderByExpression(Expression sourceExpression, ParameterExpression parameterExpression, bool first);


        /// <summary>
        /// Use this method to do initializing and validation
        /// </summary>
        internal virtual void Initialize() { }



        /// <exclude />
        public void SetOwnerNode(TreeNode treeNode)
        {
            this.OwnerNode = (DataElementsTreeNode)treeNode;
        }



        /// <exclude />
        protected void AddValidationError(string stringName, params object[] args)
        {
            this.OwnerNode.Tree.BuildResult.AddValidationError(ValidationError.Create(this.XPath, stringName, args));
        }
    }
}
