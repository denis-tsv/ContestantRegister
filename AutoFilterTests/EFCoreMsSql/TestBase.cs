using System;
using System.IO;
using System.Linq;

namespace AutoFilterTests.Querable
{
    public class TestBase
    {
        

        //private DbContextOptions<TestContext> options = new DbContextOptionsBuilder<TestContext>()
        //        .UseInMemoryDatabase(databaseName: "Test")
        //        .Options;



        protected TestContext Context { get; set; }
        private static bool initialized;
        protected void Init()
        {
            Context = new TestContext();

#if EF6
            Context.Database.Log = (s) => 
            {
                //C:\Users\DenisT\source\repos\ContestantRegister\AutoFilterTests.EF6\bin\Debug\ef6log.txt
                //File.AppendAllText("ef6log.txt", s);
                Console.Write(s);
            };
#endif
            //if (!initialized)
            //{
            //    //Context.Database.EnsureDeleted();
            //    //Context.Database.EnsureCreated();
            //    Context.ConvertItems.AddRange(Enumerable.ConverterTests.Items);
            //    Context.FilterConditionItems.AddRange(Enumerable.FilterConditionTests.Items);
            //    Context.StringTestItems.AddRange(Enumerable.StringTests.Items);
            //    Context.NestedItems.AddRange(
            //        Enumerable.NavigationPropertyTests.Items.Where(x => x.NestedItem != null).Select(x => x.NestedItem));
            //    Context.NavigationPropertyItems.AddRange(Enumerable.NavigationPropertyTests.Items);
            //    Context.CompositeKindItems.AddRange(Enumerable.CompositeKindTests.Items);
            //    Context.InvalidCaseItems.AddRange(Enumerable.InvalidCasesTests.Items);
            //    Context.SaveChanges();

            //    initialized = true;
            //}
        }
    }
}

