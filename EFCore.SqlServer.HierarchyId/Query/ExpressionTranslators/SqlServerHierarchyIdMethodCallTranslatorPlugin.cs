using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class SqlServerHierarchyIdMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
            = new[] { new SqlServerHierarchyIdMethodTranslator() };
    }
}
