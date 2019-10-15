using AutoFilter;
using AutoFilterTests.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoFilterTests.Enumerable
{
    public class CompositeKindTests
    {
        public static List<CompositeKindItem> Items = new List<CompositeKindItem>
        {
            new CompositeKindItem
            {
                Int1 = 1,
            },
            new CompositeKindItem
            {
                Int2 = 1,
            },
            new CompositeKindItem
            {
                Int1 = 1,
                Int2 = 1,
            },
        };

        [Fact]
        public void AndTest()
        {
            //arrange
            var filter = new CompositeKindFilter { Int1 = 1, Int2 = 1 };

            //act
            var filtered = Items.AutoFilter(filter, ComposeKind.And).ToList();

            //assert
            Assert.Equal(1, filtered.Count);            
        }

        [Fact]
        public void OrTest()
        {
            //arrange
            var filter = new CompositeKindFilter { Int1 = 1, Int2 = 1 };

            //act
            var filtered = Items.AutoFilter(filter, ComposeKind.Or).ToList();

            //assert
            Assert.Equal(3, filtered.Count);
        }
    }
}
