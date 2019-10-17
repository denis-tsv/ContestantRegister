using AutoFilterTests.Models;
using System.Collections.Generic;
using Xunit;
using AutoFilter;
using System.Linq;
using System;

namespace AutoFilterTests.Enumerable
{
    public class InvalidCasesTests
    {
        public static List<InvalidCaseItem> Items = new List<InvalidCaseItem>
        {
            new InvalidCaseItem
            {
                TargetEnum = TargetEnum.Default
            },
            new InvalidCaseItem
            {
                TargetEnum = TargetEnum.First
            },
        };

        [Fact]
        public void WithTargetType()
        {
            //arrange
            var filter = new InvalidCaseFilter { EnumTargetType = "Default" };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //asssert
            Assert.Equal(1, filtered.Count);
            Assert.Equal(TargetEnum.Default, filtered[0].TargetEnum);
        }

        [Fact]
        public void NotExistsValue()
        {
            //arrange
            var filter = new InvalidCaseFilter { NotExistsValue = "WrongEnumName" };

            //act
            
            //asssert
            Assert.ThrowsAny<Exception>(() => Items.AutoFilter(filter));
        }

        [Fact]
        public void NotExistsProperty()
        {
            //arrange
            var filter = new InvalidCaseFilter { NotExistsProperty = "First" };

            //act
            
            //asssert
            Assert.ThrowsAny<Exception>(() => Items.AutoFilter(filter));
        }
    }
}
