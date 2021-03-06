﻿using System;

namespace ExpressionVisualizer
{
    [Serializable]
    class ExpressionTreeContainer
    {
        // Methods
        public ExpressionTreeContainer(ExpressionNode treeNode, string expression)
        {
            this.TreeNode = treeNode;
            this.Expression = expression;
        }

        // Properties
        public string Expression { get; set; }

        public ExpressionNode TreeNode { get; set; }
    }
}