using AutoFilter;
using AutoFilterTests.Models;
using System.Linq;
using Xunit;

namespace AutoFilterTests.Querable
{
    public class StringTests : TestBase
    {
        [Fact]
        public void NoAttributes()
        {
            //arrange
            Init();
            var filter = new StringFilter { NoAttribute = "NoAttribute" };
            
            //act
            var filtered = Context.StringTestItems.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(2, filtered.Count); //для SQL Serevr like регистронезависимый 
            Assert.Equal("noattribute", filtered[0].NoAttribute); //странно, генерится SQL для регистрозависимого сравнения. Но он не работает ) 
            Assert.Equal("NoAttributeOk", filtered[1].NoAttribute);

            /*
             SELECT [Id]
      ,[NoAttribute]
	  ,LEFT(s.[NoAttribute], (LEN(N'NoAttribute')))  as L
  FROM [dbo].[StringTestItems] s
  where [NoAttribute] LIKE 'NoAttribute' + '%' AND (LEFT(s.[NoAttribute], (LEN(N'NoAttribute'))) = N'NoAttribute')

             */
        }

        [Fact]
        public void ContainsCase()
        {
            //arrange
            Init();
            var filter = new StringFilter { ContainsCase = "ContainsCase" };

            //act
            var filtered = Context.StringTestItems.AutoFilter(filter).OrderBy(x => x.ContainsCase).ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal("containscase", filtered[0].ContainsCase);
            Assert.Equal("TestContainsCase", filtered[1].ContainsCase);

            /*
             SELECT [Id], ContainsCase	  
              FROM [dbo].[StringTestItems] s
              where charindex('ContainsCase', s.ContainsCase) > 0
             */
        }

        [Fact]
        public void ContainsIgnoreCase()
        {
            //arrange
            Init();
            var filter = new StringFilter { ContainsIgnoreCase = "ContainsIgnoreCase" };

            //act
            var filtered = Context.StringTestItems.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal("testcontainsignorecase", filtered[0].ContainsIgnoreCase);
            Assert.Equal("TestContainsIgnoreCase", filtered[1].ContainsIgnoreCase);
        }

        [Fact]
        public void StartsWithCase()
        {
            //arrange
            Init();
            var filter = new StringFilter { StartsWithCase = "StartsWithCase" };

            //act
            var filtered = Context.StringTestItems.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal("startswithcase", filtered[0].StartsWithCase);
            Assert.Equal("StartsWithCase", filtered[1].StartsWithCase);
        }

        [Fact]
        public void StartsWithIgnoreCase()
        {
            //arrange
            Init();
            var filter = new StringFilter { StartsWithIgnoreCase = "StartsWithIgnoreCase" };

            //act
            var filtered = Context.StringTestItems.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal("startswithignorecasetest", filtered[0].StartsWithIgnoreCase);
            Assert.Equal("StartsWithIgnoreCaseTest", filtered[1].StartsWithIgnoreCase);
        }

        [Fact]
        public void PropertyName()
        {
            //arrange
            Init();
            var filter = new StringFilter { TargetStringProperty = "PropertyName" };

            //act
            var filtered = Context.StringTestItems.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal("PropertyName", filtered[0].PropertyName);            
        }

        [Fact]
        public void TargetStringPropertyContainsIgnoreCase()
        {
            //arrange
            Init();
            var filter = new StringFilter { TargetStringPropertyContainsIgnoreCase = "ropertyname" };

            //act
            var filtered = Context.StringTestItems.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal("PropertyName", filtered[0].PropertyName);
        }

    }
}
