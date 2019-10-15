﻿using AutoFilter;
using System;

namespace AutoFilterTests.Models
{
    public class StringFilter
    {
        public string NoAttribute { get; set; }

        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = false)]
        public string ContainsCase { get; set; }

        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string ContainsIgnoreCase { get; set; }

        [FilterProperty(StringFilter = StringFilterCondition.StartsWith, IgnoreCase = false)]
        public string StartsWithCase { get; set; }

        [FilterProperty(StringFilter = StringFilterCondition.StartsWith, IgnoreCase = true)]
        public string StartsWithIgnoreCase { get; set; }

        [FilterProperty(TargetPropertyName = "PropertyName")]
        public string TargetStringProperty { get; set; }

        [FilterProperty(TargetPropertyName = "PropertyName", StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string TargetStringPropertyContainsIgnoreCase { get; set; }        
    }
}
