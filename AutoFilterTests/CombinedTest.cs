using AutoFilter;
using AutoFilterTests.Models;
using AutoFilterTests.Querable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AutoFilterTests
{
    public class CombinedTest : TestBase
    {
        [Fact]
        public void TestSqlAndInMemoryQuery()
        {
            //arrange
            Init();
            var filter = new StringFilter { NoAttribute = "NoAttribute" };

            //act
            var filteredDatabase = Context.StringTestItems.AutoFilter(filter).ToList();
            var filteredInMemory = Enumerable.StringTestsData.Items.AutoFilter(filter).ToList();


            //assert
            Assert.Equal(2, filteredDatabase.Count);
            Assert.Equal(1, filteredInMemory.Count);
        }
    }
}
