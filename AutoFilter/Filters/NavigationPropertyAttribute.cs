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

        //TODO не потокобезопасно. решается кешированием GetPropertyExpression в базовом классе
        Expression _aggregatedNullChecks;

        protected override Expression GetNestedNullCheckExpression(Expression propertyExpression)
        {
            return _aggregatedNullChecks;
        }

        protected override MemberExpression GetPropertyExpression(ParameterExpression parameter, bool inMemory, PropertyInfo filterPropertyInfo)
        {
            var propNames = Path.Split('.');
            var nullchecks = new List<Expression>();
            var property = Expression.Property(parameter, propNames[0]);
            var nullExpression = Expression.Constant(null);

            if (inMemory)
            {
                var nullCheck = Expression.NotEqual(property, nullExpression);
                nullchecks.Add(nullCheck);
            }
            for (int i = 1; i < propNames.Length; i++)
            {
                property = Expression.Property(property, propNames[i]);
                if (inMemory)
                {
                    var nullCheck = Expression.NotEqual(property, nullExpression);
                    nullchecks.Add(nullCheck);
                }
            }

            if (nullchecks.Any())
            {
                _aggregatedNullChecks = nullchecks.Aggregate((cur, next) => Expression.AndAlso(cur, next));
            }

            return Expression.Property(property, GetPropertyName(filterPropertyInfo));
        }
    }
}
