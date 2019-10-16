﻿using AutoFilter;
using AutoFilterTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoFilterTests.Enumerable
{
    public class FilterConditionTests
    {
        public static List<FilterConditionItem> Items = new List<FilterConditionItem>
        {
            //BoolEqual
            new FilterConditionItem
            {

            },
            new FilterConditionItem
            {
                BoolEqual = true
            },
            new FilterConditionItem
            {
                BoolEqual = false
            },

            //IntGreaterOrEqual
            new FilterConditionItem
            {
                IntGreaterOrEqual = 1
            },
            new FilterConditionItem
            {
                IntGreaterOrEqual = 2
            },
            new FilterConditionItem
            {
                IntGreaterOrEqual = -1
            },

            //DateTimeLessOrEqual
            new FilterConditionItem
            {
                DateTimeLessOrEqual = new DateTime(2010, 10, 23, 14, 56, 54)
            },
            new FilterConditionItem
            {
                DateTimeLessOrEqual = new DateTime(2015, 10, 10)
            },
            new FilterConditionItem
            {
                DateTimeLessOrEqual = new DateTime(2010, 10, 23)
            },

            //DecimalLess
            new FilterConditionItem
            {
                DecimalLess = 1000
            },
            new FilterConditionItem
            {
                DecimalLess = -1000
            },
            new FilterConditionItem
            {
                DecimalLess = -1
            },

            //DoubleGreater
            new FilterConditionItem
            {
                DoubleGreater = 1000
            },
            new FilterConditionItem
            {
                DoubleGreater = -1000
            },
            new FilterConditionItem
            {
                DoubleGreater = 1
            },
        };

        [Fact]
        public void BoolEqual()
        {
            //arrange
            var filter = new FilterConditionFilter { BoolEqual = true };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal(true, filtered[0].BoolEqual);
        }

        [Fact] 
        public void IntGreaterOrEqual()
        {
            //arrange
            var filter = new FilterConditionFilter { IntGreaterOrEqual = 1 };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal(1, filtered[0].IntGreaterOrEqual);
            Assert.Equal(2, filtered[1].IntGreaterOrEqual);
        }

        [Fact] 
        public void DateTimeLessOrEqual()
        {
            //arrange
            var filter = new FilterConditionFilter { DateTimeLessOrEqual = new DateTime(2010, 10, 23, 14, 56, 54) };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(2, filtered.Count);
            Assert.Equal(new DateTime(2010, 10, 23, 14, 56, 54), filtered[0].DateTimeLessOrEqual);
            Assert.Equal(new DateTime(2010, 10, 23), filtered[1].DateTimeLessOrEqual);
        }

        [Fact]
        public void DecimalLess()
        {
            //arrange
            var filter = new FilterConditionFilter { DecimalLess = -1 };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal(-1000, filtered[0].DecimalLess);            
        }
        

        [Fact]
        public void DoubleGreater()
        {
            //arrange
            var filter = new FilterConditionFilter { DoubleGreater = 1 };

            //act
            var filtered = Items.AutoFilter(filter).ToList();

            //assert
            Assert.Equal(1, filtered.Count);
            Assert.Equal(1000, filtered[0].DoubleGreater);
        }
    }
}