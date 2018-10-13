using Microsoft.SqlServer.Types;

namespace Bricelam.EntityFrameworkCore.Test.Models
{
    class Patriarch
    {
        public SqlHierarchyId Id { get; set; }
        public string Name { get; set; }
    }
}
