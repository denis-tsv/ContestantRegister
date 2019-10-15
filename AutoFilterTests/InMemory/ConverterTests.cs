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
        public static List<ConvertItem> Items = new List<ConvertItem>
        {
            new ConvertItem
            {

            },

            new ConvertItem
            {
                WithConverter = true,
            },
            new ConvertItem
            {
                WithConverter = false,
            },

            new ConvertItem
            {
                WithoutConverter = true,
            },
            new ConvertItem
            {
                WithoutConverter = false,
            },
        };

        [Fact]
        public void WithConverter()
        {
            //arrange
            var filter = new ConvertFilter { WithConverter = 1 };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

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
            Assert.Throws<InvalidOperationException>(() => Items.AutoFilter(filter));            
        }

    }
}
