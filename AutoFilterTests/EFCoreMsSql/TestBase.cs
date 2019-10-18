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
            //    Context.ConvertItems.AddRange(Enumerable.ConverterTestsData.Items);
            //    Context.FilterConditionItems.AddRange(Enumerable.FilterConditionTestsData.Items);
            //    Context.StringTestItems.AddRange(Enumerable.StringTestsData.Items);
            //    Context.NestedItems.AddRange(
            //        Enumerable.NavigationPropertyTestsData.Items.Where(x => x.NestedItem != null).Select(x => x.NestedItem));
            //    Context.NavigationPropertyItems.AddRange(Enumerable.NavigationPropertyTestsData.Items);
            //    Context.CompositeKindItems.AddRange(Enumerable.CompositeKindTestsData.Items);
            //    Context.InvalidCaseItems.AddRange(Enumerable.InvalidCasesTestsData.Items);
            //    Context.SaveChanges();

            //    initialized = true;
            //}
        }
    }
}

