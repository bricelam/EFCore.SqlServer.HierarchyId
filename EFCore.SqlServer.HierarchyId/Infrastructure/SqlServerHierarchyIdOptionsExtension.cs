using Bricelam.EntityFrameworkCore.Properties;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.SqlServer.Infrastructure
{
    internal class SqlServerHierarchyIdOptionsExtension : IDbContextOptionsExtensionWithDebugInfo
    {
        public virtual string LogFragment
            => "using HierarchyId ";

        public virtual bool ApplyServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServerHierarchyId();

            return false;
        }

        public virtual long GetServiceProviderHashCode()
            => 0;

        public virtual void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["SqlServer:" + nameof(SqlServerHierarchyIdDbContextOptionsBuilderExtensions.UseHierarchyId)] = "1";

        public virtual void Validate(IDbContextOptions options)
        {
            var internalServiceProvider = options.FindExtension<CoreOptionsExtension>()?.InternalServiceProvider;
            if (internalServiceProvider != null)
            {
                using (var scope = internalServiceProvider.CreateScope())
                {
                    if (scope.ServiceProvider.GetService<IEnumerable<IMethodCallTranslatorPlugin>>()
                            ?.Any(s => s is SqlServerHierarchyIdMethodCallTranslatorPlugin) != true)
                    {
                        throw new InvalidOperationException(Resources.ServicesMissing);
                    }
                }
            }
        }
    }
}
