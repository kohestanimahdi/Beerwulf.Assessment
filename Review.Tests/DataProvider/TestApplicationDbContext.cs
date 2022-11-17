using Microsoft.EntityFrameworkCore;
using Review.Infrastructure.Persistance;
using System;

namespace Review.Tests.DataProvider
{
    internal class TestApplicationDbContext : ApplicationDbContext, IDisposable
    {
        private static DbContextOptionsBuilder _optionsBuilder = new();
        public TestApplicationDbContext(string dbName) : base(_optionsBuilder.UseInMemoryDatabase($"Test_{dbName}").Options)
        {
        }
    }
}
