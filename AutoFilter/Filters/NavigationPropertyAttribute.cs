using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoFilter.Filters
{
    public class NavigationPropertyAttribute : FilterPropertyAttribute
    {
        public NavigationPropertyAttribute(string objectName)
        {
            Path = objectName ?? throw new ArgumentNullException(nameof(objectName));
        }

        public string Path { get; }

        private Expression _aggregatedNullChecks;
        private static Expression _nullConstant = Expression.Constant(null);

        protected override Expression GetNestedNullCheckExpression(Expression propertyExpression)
        {
            if (_aggregatedNullChecks == null)
            {
                var nullchecks = new List<Expression>();
                var propNames = Path.Split('.');
                var property = Expression.Property(_parameter, propNames[0]);

                var nullCheck = Expression.NotEqual(property, _nullConstant);
                nullchecks.Add(nullCheck);

                for (int i = 1; i < propNames.Length; i++)
                {
                    property = Expression.Property(property, propNames[i]);
                    nullCheck = Expression.NotEqual(property, _nullConstant);
                    nullchecks.Add(nullCheck);                    
                }

                _aggregatedNullChecks = nullchecks.Aggregate((cur, next) => Expression.AndAlso(cur, next));
            }
            return _aggregatedNullChecks;
        }

        protected override MemberExpression GetPropertyExpression(ParameterExpression parameter, PropertyInfo filterPropertyInfo)
        {
            var propNames = Path.Split('.');
            
            var property = Expression.Property(parameter, propNames[0]);
            
            for (int i = 1; i < propNames.Length; i++)
            {
                property = Expression.Property(property, propNames[i]);                
            }

            return Expression.Property(property, GetPropertyName(filterPropertyInfo));
        }
    }
}
