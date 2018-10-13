using System;
using System.Linq;
using Bricelam.EntityFrameworkCore.Test.Models;
using Xunit;

namespace Bricelam.EntityFrameworkCore
{
    public class QueryTests : IDisposable
    {
        private readonly AbrahamicContext _db;

        public QueryTests()
        {
            _db = new AbrahamicContext();
            _db.Database.EnsureCreated();
        }

        [Fact]
        public void GetLevel_works()
        {
            var results = Enumerable.ToList(
                from p in _db.Patriarchy
                where (bool)(p.Id.GetLevel() == 0)
                select p.Name);

            Assert.Equal(
                @"
                    SELECT [p].[Id], [p].[Name]
                    FROM [Patriarchy] AS [p]
                    WHERE [p].[Id].GetLevel = 0
                ",
                _db.Sql,
                ignoreWhiteSpaceDifferences: true);
            //Assert.Equal(new[] { "Abraham" }, results);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
