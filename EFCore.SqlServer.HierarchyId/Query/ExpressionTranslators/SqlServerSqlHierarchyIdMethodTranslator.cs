using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.SqlServer.Types;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class SqlServerSqlHierarchyIdMethodTranslator : IMethodCallTranslator
    {
        private static readonly IDictionary<MethodInfo, string> _methodToFunctionName = new Dictionary<MethodInfo, string>
        {
            { typeof(SqlHierarchyId).GetRuntimeMethod(nameof(SqlHierarchyId.GetAncestor), new[] { typeof(int) }), "GetAncestor" },
            { typeof(SqlHierarchyId).GetRuntimeMethod(nameof(SqlHierarchyId.GetDescendant), new[] { typeof(SqlHierarchyId), typeof(SqlHierarchyId) }), "GetDescendant" },
            { typeof(SqlHierarchyId).GetRuntimeMethod(nameof(SqlHierarchyId.GetLevel), Type.EmptyTypes), "GetLevel" },
            { typeof(SqlHierarchyId).GetRuntimeMethod(nameof(SqlHierarchyId.GetReparentedValue), new[] { typeof(SqlHierarchyId), typeof(SqlHierarchyId) }), "GetReparentedValue" },
            { typeof(SqlHierarchyId).GetRuntimeMethod(nameof(SqlHierarchyId.GetRoot), Type.EmptyTypes), "GetRoot" },
            { typeof(SqlHierarchyId).GetRuntimeMethod(nameof(SqlHierarchyId.IsDescendantOf), new[] { typeof(SqlHierarchyId) }), "IsDescendantOf" }
        };

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (_methodToFunctionName.TryGetValue(methodCallExpression.Method, out var functionName))
            {
                return new SqlFunctionExpression(
                    methodCallExpression.Object,
                    functionName,
                    methodCallExpression.Type,
                    methodCallExpression.Arguments);
            }

            return null;
        }
    }
}
