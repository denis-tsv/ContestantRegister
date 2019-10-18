using AutoFilterTests.Models;
using System.Collections.Generic;

namespace AutoFilterTests.Enumerable
{
    public class InvalidCasesTestsData
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

       
    }
}
