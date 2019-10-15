using System;
using AutoFilter.Filters.Convert;
using ContestantRegister.Framework.Extensions;

namespace ContestantRegister.Cqrs.Features._Common.ListViewModel
{
    public class EnumDisplayToValueConverter<TEnum> : IFilverValueConverter
    {
        public object Convert(object v)
        {
            var value = (string)v;

            var enumType = typeof(TEnum);
            object res = null;
            foreach (var enumVal in Enum.GetValues(enumType))
            {
                if (FrameworkExtensions.GetEnumDisplayName(enumType, enumVal).StartsWith(value, StringComparison.OrdinalIgnoreCase))
                {
                    if (res != null) return null; // two or more corresponding items found. in this case filter will not work
                    res = enumVal;
                }
            }                
            
            return res;
        }
    }
}
