using AutoFilter;
using AutoFilterTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoFilterTests.Enumerable
{
    public class ConverterTests
    {
        [Fact]
        public void WithConverter()
        {
            //arrange
            var filter = new ConvertFilter { WithConverter = 1 };

            //act
            var filtered = ConverterTestsData.Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal(true, filtered[0].WithConverter);
        }

        [Fact]
        public void WithoutConverter()
        {
            //arrange
            var filter = new ConvertFilter { WithoutConverter = 0 };

            //act
            
            //assert
            Assert.ThrowsAny<Exception>(() => ConverterTestsData.Items.AutoFilter(filter));            
        }

    }
}
