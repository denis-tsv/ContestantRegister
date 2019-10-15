using System.Reflection;

namespace AutoFilter
{
    internal class FilterProperty
    {
        public PropertyInfo PropertyInfo { get; set; }

        public FilterPropertyAttribute FilterPropertyAttribute { get; set; }        
    }
}
