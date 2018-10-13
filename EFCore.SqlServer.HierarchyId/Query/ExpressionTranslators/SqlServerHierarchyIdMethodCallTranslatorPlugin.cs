using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class SqlServerHierarchyIdMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
            = new[] { new SqlServerSqlHierarchyIdMethodTranslator() };
    }
}
