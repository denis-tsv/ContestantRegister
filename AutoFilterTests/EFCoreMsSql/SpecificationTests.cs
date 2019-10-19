using AutoFilter;
using AutoFilterTests.Enumerable;
using AutoFilterTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AutoFilterTests.Querable
{
    public class SpecificationTests : TestBase
    {
        private Spec<FilterConditionItem> _intGreatOrEqual1 = new Spec<FilterConditionItem>(x => x.IntGreaterOrEqual >= 1);
        
        [Fact]
        public void AndTest()
        {
            //arrange
            var boolIsEmpty = new Spec<FilterConditionItem>(x => !x.BoolEqual.HasValue);
            Init();
            
            //act
            var filtered = Context.FilterConditionItems.Where(_intGreatOrEqual1 && boolIsEmpty).ToList();

            //assert
            Assert.Equal(2, filtered.Count);
        }

        [Fact]
        public void OrTest()
        {
            //arrange 
            var _decimalless0 = new Spec<FilterConditionItem>(x => x.DecimalLess < 0);
            Init();

            //act
            var filtered = Context.FilterConditionItems.Where(_intGreatOrEqual1 || _decimalless0).ToList();

            //assert
            Assert.Equal(4, filtered.Count);
        }

        [Fact]
        public void NotTest()
        {
            //arrange 
            Init();
            var _decimalless1 = new Spec<FilterConditionItem>(x => x.DecimalLess < 1);

            //act
            var filtered = Context.FilterConditionItems.Where(!_decimalless1).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
        }
    }
}
