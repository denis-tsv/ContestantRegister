using AutoFilter;
using AutoFilterTests.Models;
using System;
using System.Linq;
using Xunit;

namespace AutoFilterTests.Querable
{
    public class ConverterTests : TestBase
    {
        [Fact]
        public void WithConverter()
        {
            //arrange
            Init();
            var filter = new ConvertFilter { WithConverter = 1 };

            //act
            var filtered = Context.ConvertItems.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal(true, filtered[0].WithConverter);
        }

        [Fact]
        public void WithoutConverter()
        {
            //arrange
            Init();
            var filter = new ConvertFilter { WithoutConverter = 0 };

            //act

            //assert
            Assert.ThrowsAny<Exception>(() => Context.ConvertItems.AutoFilter(filter));
        }
    }
}
