using System.IO;
using System.Linq.Expressions;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ExpressionVisualizer
{
    class ExpressionTreeVisualizerObjectSource : VisualizerObjectSource
    {
        // Methods
        public override void GetData(object target, Stream outgoingData)
        {
            Expression expression = (Expression)target;
            ExpressionTreeContainer container = new ExpressionTreeContainer(new ExpressionNode(expression), expression.ToString());
            Serialize(outgoingData, container);
        }
    }
}