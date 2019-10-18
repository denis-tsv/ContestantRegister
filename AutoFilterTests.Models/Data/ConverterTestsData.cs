using AutoFilterTests.Models;
using System.Collections.Generic;

namespace AutoFilterTests.Enumerable
{
    public class ConverterTestsData
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

    }
}
