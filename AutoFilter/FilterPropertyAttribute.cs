using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoFilter
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FilterPropertyAttribute : Attribute
    {
        private static string _sync = "sync";

        private static Expression _nullConstant = Expression.Constant(null);

        public string TargetPropertyName { get; set; }

        //нигде не используется, можно выпилить ) или придумать кейс когда он нужен и написать на него тест
        public Type TargetType { get; set; }

        public StringFilterCondition StringFilter { get; set; } // в атрибуте нельзя задать Nullable свойство значимого типа 

        public bool IgnoreCase { get; set; }

        public FilterCondition FilterCondition { get; set; }

        //TODO удостовеиться, что CLR создает новый класс на каждый generic метод 
        private MemberExpression _property;
        private Expression _propertyNullCheck;
        protected ParameterExpression _parameter;
        private bool _initialized;
        public virtual Expression<Func<TItem, bool>> GetExpression<TItem>(bool inMemory, PropertyInfo filterPropertyInfo, object filter)
        {
            lock(_sync)//нужен ли этот кеш? 
            {
                if (!_initialized)
                {
                    _parameter = Expression.Parameter(typeof(TItem));
                    _property = GetPropertyExpression(_parameter, filterPropertyInfo);
                    
                    _initialized = true;
                }   
            }
            
            Expression value = Expression.Constant(GetPropertyValue(filterPropertyInfo, filter));

            value = Expression.Convert(value, _property.Type); //например для конвертирования enum в object или int? в int

            var body = GetBody(_property, value, inMemory, filterPropertyInfo);

            if (inMemory)
            {
                var nullChecks = new List<Expression>();
                
                var nestedNullCheck = GetNestedNullCheckExpression(_property);//кеш в атрибуте NavigationProperty
                if (nestedNullCheck != null) nullChecks.Add(nestedNullCheck);

                //для Nullable ValueType не гегерируется проверка самогосвойства на null, но и без этого работает
                if (!GetPropertyType(filterPropertyInfo).IsValueType)
                {
                    if (_propertyNullCheck == null)
                        _propertyNullCheck = GetNullCheckExpression(_property);
                    nullChecks.Add(_propertyNullCheck);
                }
                if (nullChecks.Any())
                {
                    nullChecks.Add(body);
                    body = nullChecks.Aggregate((x, y) => Expression.AndAlso(x, y));
                }                
            }

            var res = Expression.Lambda<Func<TItem, bool>>(body, _parameter);

            return res;
        }

        protected virtual Expression GetNullCheckExpression(Expression propertyExpression)
        {
            return Expression.NotEqual(propertyExpression, _nullConstant);
        }

        protected virtual Expression GetNestedNullCheckExpression(Expression propertyExpression)
        {
            return null;
        }

        protected virtual object GetPropertyValue(PropertyInfo filterPropertyInfo, object filter)
        {
            return filterPropertyInfo.GetValue(filter);
        }

        protected virtual Type GetPropertyType(PropertyInfo filterPropertyInfo)
        {
            return TargetType ?? filterPropertyInfo.PropertyType;
        }

        protected virtual string GetPropertyName(PropertyInfo filterPropertyInfo)
        {
            return TargetPropertyName ?? filterPropertyInfo.Name;
        }

        protected virtual Expression GetBody(MemberExpression property, Expression value, bool inMemory, PropertyInfo filterPropertyInfo)
        {
            //TODO тоже можно закешировать, но эффекта практически не будет, и так уже все закешировано,
            var func = GetBodyBuilderFunc(filterPropertyInfo);

            return func(property, value);
        }

        protected virtual MemberExpression GetPropertyExpression(ParameterExpression parameter, PropertyInfo filterPropertyInfo)
        {
            return Expression.Property(parameter, GetPropertyName(filterPropertyInfo));
        }

        protected virtual Func<MemberExpression, Expression, Expression> GetBodyBuilderFunc(PropertyInfo filterPropertyInfo)
        {
            var propertyType = GetPropertyType(filterPropertyInfo);

            if (propertyType == typeof(string))
                return GetStringBuilderFunc();

            switch (FilterCondition)
            {
                case FilterCondition.Equal: return Expression.Equal;
                case FilterCondition.Greater: return Expression.GreaterThan;
                case FilterCondition.GreaterOrEqual: return Expression.GreaterThanOrEqual;
                case FilterCondition.Less: return Expression.LessThan;
                case FilterCondition.LessOrEqual: return Expression.LessThanOrEqual;
            }

            return Expression.Equal;        
        }
        
        private static readonly string StringStartWithIgnoreCase = "StringStartWithIgnoreCase";
        private static readonly string StringStartWith = "StringStartWith";
        private static readonly string StringContainsIgnoreCase = "StringContainsIgnoreCase";
        private static readonly string StringContains = "StringContains";

        private static readonly IReadOnlyDictionary<string, Func<MemberExpression, Expression, Expression>> _stringFilters;            

        static FilterPropertyAttribute()
        {
            var filters = new Dictionary<string, Func<MemberExpression, Expression, Expression>>();

            var startsWith = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            var contains = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            filters.Add(StringStartWith, (p, v) => Expression.Call(p, startsWith, v));
            filters.Add(StringContains, (p, v) => Expression.Call(p, contains, v));

            Func<MemberExpression, Expression, Expression> startIgnoreCase = (p, v) =>
            {
                var mi = typeof(string).GetMethod("ToLower", new Type[] { });
                var pl = Expression.Call(p, mi);
                var vl = Expression.Call(v, mi);
                return Expression.Call(pl, startsWith, vl);
            };
            filters.Add(StringStartWithIgnoreCase, startIgnoreCase);

            Func<MemberExpression, Expression, Expression> containsIgnoreCase = (p, v) =>
            {
                var mi = typeof(string).GetMethod("ToLower", new Type[] { });
                var pl = Expression.Call(p, mi);
                var vl = Expression.Call(v, mi);
                return Expression.Call(pl, contains, vl);
            };
            filters.Add(StringContainsIgnoreCase, containsIgnoreCase);
            _stringFilters = filters;
        }

        protected virtual Func<MemberExpression, Expression, Expression> GetStringBuilderFunc()
        {
            //TODO use C# 8 tuple patterns
            switch (StringFilter)
            {
                case StringFilterCondition.StartsWith:
                    if (IgnoreCase)
                        return _stringFilters[StringStartWithIgnoreCase];
                    else
                        return _stringFilters[StringStartWith];                    
                case StringFilterCondition.Contains:
                    if (IgnoreCase)
                        return _stringFilters[StringContainsIgnoreCase];
                    else
                        return _stringFilters[StringContains];                    
            }

            throw new InvalidOperationException();
        }
    }
}
