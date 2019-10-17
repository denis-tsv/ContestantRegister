﻿using AutoFilter.Filters.Convert;
using System;

namespace AutoFilterTests.Models
{
    public class StringToEnumConverter : IFilverValueConverter
    {
        public object Convert(object value)
        {
            return Enum.Parse<TargetEnum>((string)value);
        }
    }

    public class InvalidCaseFilter
    {
        [ConvertFilter(typeof(StringToEnumConverter), TargetPropertyName = "TargetEnum")]
        public string EnumTargetType { get; set; }

        [ConvertFilter(typeof(StringToEnumConverter), TargetPropertyName = "TargetEnum")]
        public string NotExistsValue { get; set; }

        public string NotExistsProperty { get; set; }
    }
}
