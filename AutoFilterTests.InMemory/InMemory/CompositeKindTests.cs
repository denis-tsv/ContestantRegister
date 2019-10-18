﻿using AutoFilter;
using AutoFilterTests.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoFilterTests.Enumerable
{
    public class CompositeKindTests
    {
        
        [Fact]
        public void AndTest()
        {
            //arrange
            var filter = new CompositeKindFilter { Int1 = 1, Int2 = 1 };

            //act
            var filtered = CompositeKindTestsData.Items.AutoFilter(filter, ComposeKind.And).ToList();

            //assert
            Assert.Equal(1, filtered.Count);            
        }

        [Fact]
        public void OrTest()
        {
            //arrange
            var filter = new CompositeKindFilter { Int1 = 1, Int2 = 1 };

            //act
            var filtered = CompositeKindTestsData.Items.AutoFilter(filter, ComposeKind.Or).ToList();

            //assert
            Assert.Equal(3, filtered.Count);
        }
    }
}
