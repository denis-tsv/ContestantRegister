using AutoFilter;
using AutoFilterTests.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoFilterTests.Enumerable
{
    public class NavigationPropertyTests
    {
        public static List<NavigationPropertyItem> Items = new List<NavigationPropertyItem>
        {
            new NavigationPropertyItem
            {

            },
            new NavigationPropertyItem
            {
                NestedItem = new NestedItem
                {
                }
            },
            
            //Int
            new NavigationPropertyItem
            {
                NestedItem = new NestedItem
                {
                    Int = 1
                }
            },
            new NavigationPropertyItem
            {
                NestedItem = new NestedItem
                {
                    Int = 2
                }
            },

            //NullableInt
            new NavigationPropertyItem
            {
                NestedItem = new NestedItem
                {
                    NullableInt = 1
                }
            },
            new NavigationPropertyItem
            {
                NestedItem = new NestedItem
                {
                    NullableInt = 2
                }
            },
            new NavigationPropertyItem
            {
                NestedItem = new NestedItem
                {
                    NullableInt = -1
                }
            },

            //string
             new NavigationPropertyItem
            {
                NestedItem = new NestedItem
                {
                    String = "Nested"
                }
            },
            new NavigationPropertyItem
            {
                NestedItem = new NestedItem
                {
                    String = "NotInResult"
                }
            },           
            
        };


        [Fact]
        public void IntEqual()
        {
            //arrange
            var filter = new NavigationPropertyFilter { Int = 1 };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal(1, filtered[0].NestedItem.Int);
        }

        [Fact]
        public void NullableIntGreatOrEqual()
        {
            //arrange
            var filter = new NavigationPropertyFilter { NullableIntFilter = 1 };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal(1, filtered[0].NestedItem.NullableInt);
            Assert.Equal(2, filtered[1].NestedItem.NullableInt);
        }

        [Fact]
        public void NestedString()
        {
            //arrange
            var filter = new NavigationPropertyFilter { StringFilter = "Nested" };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal("Nested", filtered[0].NestedItem.String);
        }
    }
}
