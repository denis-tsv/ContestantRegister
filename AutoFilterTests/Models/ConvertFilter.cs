using AutoFilter.Filters.Convert;

namespace AutoFilterTests.Models
{
    public class ConvertFilter
    {
        [ConvertFilter(typeof(NullableIntToNullableBooleanConverter))]
        public int? WithConverter { get; set; }

        public int? WithoutConverter { get; set; }
    }
}
