using AutoFilterTests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AutoFilterTests.Querable
{
    public class TestContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((_, __) => true, true)
        });

        //public TestContext(DbContextOptions options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseLoggerFactory(loggerFactory)  //tie-up DbContext with LoggerFactory object
            .EnableSensitiveDataLogging()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AutoFilter;Trusted_Connection=True;MultipleActiveResultSets=true");
            //.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=AutoFilter;Integrated Security=True");

        public DbSet<ConvertItem> ConvertItems { get; set; }
        public DbSet<FilterConditionItem> FilterConditionItems { get; set; }
        public DbSet<NavigationPropertyItem> NavigationPropertyItems { get; set; }
        public DbSet<NestedItem> NestedItems { get; set; }
        public DbSet<StringTestItem> StringTestItems { get; set; }
        public DbSet<CompositeKindItem> CompositeKindItems { get; set; }
        public DbSet<InvalidCaseItem> InvalidCaseItems { get; set; }
    }
}
