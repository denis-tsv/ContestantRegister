using AutoFilter;
using AutoFilterTests.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoFilterTests.Enumerable
{
    public class StringTests
    {
        public static List<StringTestItem> Items = new List<StringTestItem>
        {
            //NoAttributes
            new StringTestItem
            {
                NoAttribute = "NoAttributeOk"
            },
            new StringTestItem
            {
                NoAttribute = "NoNoAttribute"
            },
            new StringTestItem
            {
                NoAttribute = "noattribute"
            },
            new StringTestItem
            {
                NoAttribute = "nonoattribute"
            },

            //contains case
            new StringTestItem
            {
                ContainsCase = "TestContainsCase"
            },
            new StringTestItem
            {
                ContainsCase = "containscase"
            },       
            
            //ContainsIgnoreCase
            new StringTestItem
            {
                ContainsIgnoreCase = "TestContainsIgnoreCase"
            },
            new StringTestItem
            {
                ContainsIgnoreCase = "testcontainsignorecase"
            },
            new StringTestItem
            {
                ContainsIgnoreCase = "Not contains ignore case"
            },
            
            //StartsWithCase
            new StringTestItem
            {
                StartsWithCase = "StartsWithCase"
            },
            new StringTestItem
            {
                StartsWithCase = "startswithcase"
            },
            new StringTestItem
            {
                StartsWithCase = "NotStartsWithCase"
            },
            new StringTestItem
            {
                StartsWithCase = "notntartswithcase"
            },

            //StartsWithIgnoreCase
             new StringTestItem
            {
                StartsWithIgnoreCase = "StartsWithIgnoreCaseTest"
            },
            new StringTestItem
            {
                StartsWithIgnoreCase = "startswithignorecasetest"
            },
            new StringTestItem
            {
                StartsWithIgnoreCase = "NotStartsWithIgnoreCase"
            },
            new StringTestItem
            {
                StartsWithIgnoreCase = "notstartswithIgnoreCase"
            },

            //PropertyName
            new StringTestItem
            {
                PropertyName = "PropertyName"
            },
            new StringTestItem
            {
                TargetStringProperty = "PropertyName"
            },

            
            new StringTestItem
            {
                
            },

        };

        [Fact]
        public void NoAttributes()
        {
            //arrange
            var filter = new StringFilter { NoAttribute = "NoAttribute" };
            
            //act
            var filtered = Items.AutoFilter(filter);

            //assert
            Assert.Single(filtered);
            Assert.Equal("NoAttributeOk", filtered.Single().NoAttribute);
        }

        [Fact]
        public void ContainsCase()
        {
            //arrange
            var filter = new StringFilter { ContainsCase = "ContainsCase" };

            //act
            var filtered = Items.AutoFilter(filter);

            //assert
            Assert.Single(filtered);
            Assert.Equal("TestContainsCase", filtered.Single().ContainsCase);
        }

        [Fact]
        public void ContainsIgnoreCase()
        {
            //arrange
            var filter = new StringFilter { ContainsIgnoreCase = "ContainsIgnoreCase" };

            //act
            var filtered = Items.AutoFilter(filter)
                .OrderBy(x => x.ContainsIgnoreCase)
                .ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal("testcontainsignorecase", filtered[0].ContainsIgnoreCase);
            Assert.Equal("TestContainsIgnoreCase", filtered[1].ContainsIgnoreCase);
            
        }

        [Fact]
        public void StartsWithCase()
        {
            //arrange
            var filter = new StringFilter { StartsWithCase = "StartsWithCase" };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal("StartsWithCase", filtered[0].StartsWithCase);            
        }

        [Fact]
        public void StartsWithIgnoreCase()
        {
            //arrange
            var filter = new StringFilter { StartsWithIgnoreCase = "StartsWithIgnoreCase" };

            //act
            var filtered = Items.AutoFilter(filter)
                .OrderBy(x => x.StartsWithIgnoreCase)
                .ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal("startswithignorecasetest", filtered[0].StartsWithIgnoreCase);
            Assert.Equal("StartsWithIgnoreCaseTest", filtered[1].StartsWithIgnoreCase);
            
        }

        [Fact]
        public void PropertyName()
        {
            //arrange
            var filter = new StringFilter { TargetStringProperty = "PropertyName" };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal("PropertyName", filtered[0].PropertyName);            
        }

        [Fact]
        public void TargetStringPropertyContainsIgnoreCase()
        {
            //arrange
            var filter = new StringFilter { TargetStringPropertyContainsIgnoreCase = "ropertyname" };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal("PropertyName", filtered[0].PropertyName);
        }


        

    }
}
