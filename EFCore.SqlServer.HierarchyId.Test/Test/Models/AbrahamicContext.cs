using Bricelam.EntityFrameworkCore.Test.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Types;

namespace Bricelam.EntityFrameworkCore.Test.Models
{
    class AbrahamicContext : DbContext
    {
        readonly TestLoggerFactory _loggerFactory
            = new TestLoggerFactory();

        public DbSet<Patriarch> Patriarchy { get; set; }

        public string Sql
            => _loggerFactory.Logger.Sql;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseSqlServer(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HierarchyIdTests",
                    x => x.UseHierarchyId())
                .UseLoggerFactory(_loggerFactory);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Entity<Patriarch>()
                /*.HasData(
                    new Patriarch { Id = SqlHierarchyId.GetRoot(), Name = "Abraham" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/"), Name = "Isaac" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/"), Name = "Jacob" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/1/"), Name = "Reuben" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/2/"), Name = "Simeon" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/3/"), Name = "Levi" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/4/"), Name = "Judah" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/5/"), Name = "Issachar" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/6/"), Name = "Zebulun" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/7/"), Name = "Dan" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/8/"), Name = "Naphtali" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/9/"), Name = "Gad" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/10/"), Name = "Asher" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/11.1/"), Name = "Ephraim" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/11.2/"), Name = "Manasseh" },
                    new Patriarch { Id = SqlHierarchyId.Parse("/1/1/12/"), Name = "Benjamin" })*/;
    }
}
