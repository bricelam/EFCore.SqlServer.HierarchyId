using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Bricelam.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class SqlServerHierarchyIdMethodTranslator : IMethodCallTranslator
    {
        private static readonly IDictionary<MethodInfo, string> _methodToFunctionName = new Dictionary<MethodInfo, string>
        {
            { typeof(HierarchyId).GetRuntimeMethod(nameof(HierarchyId.GetAncestor), new[] { typeof(int) }), "GetAncestor" },
            { typeof(HierarchyId).GetRuntimeMethod(nameof(HierarchyId.GetDescendant), new[] { typeof(HierarchyId), typeof(HierarchyId) }), "GetDescendant" },
            { typeof(HierarchyId).GetRuntimeMethod(nameof(HierarchyId.GetLevel), Type.EmptyTypes), "GetLevel" },
            { typeof(HierarchyId).GetRuntimeMethod(nameof(HierarchyId.GetReparentedValue), new[] { typeof(HierarchyId), typeof(HierarchyId) }), "GetReparentedValue" },
            { typeof(HierarchyId).GetRuntimeMethod(nameof(HierarchyId.GetRoot), Type.EmptyTypes), "GetRoot" },
            { typeof(HierarchyId).GetRuntimeMethod(nameof(HierarchyId.IsDescendantOf), new[] { typeof(HierarchyId) }), "IsDescendantOf" }
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
