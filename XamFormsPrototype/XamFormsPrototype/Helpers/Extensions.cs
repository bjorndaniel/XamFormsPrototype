using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using XamFormsPrototype.UI.Validation;

namespace XamFormsPrototype.Helpers
{
    public static class Extensions
    {
        public static TResult Do<TTarget, TResult>(this TTarget target, Func<TTarget, TResult> action) => 
            action(target);

        public static IEnumerable<PropertyInfo> GetPropertiesWithInterface<TInterface>(this Type type) =>
            type.GetRuntimeProperties().Where(_ => typeof(TInterface).IsAssignableFrom(_.PropertyType));

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action) => 
            enumeration.ToList().ForEach(action);

        public static PropertyInfo GetPropertyWithInterface<TInterface>(this Type type, string propertyName) =>
            type.GetRuntimeProperties().FirstOrDefault(_ => 
                typeof(TInterface).IsAssignableFrom(_.PropertyType) && _.Name == propertyName
            );

        public static PropertyInfo GetPropertyWithInterface<TInterface>(this Type target, Expression<Func<object, bool>> property)
        {
            var name = GetMemberName(property);
            return target.GetPropertyWithInterface<TInterface>(name);
        }

        public static string GetMemberName(Expression expression)
        {
            switch (expression)
            {
                case LambdaExpression lambda:
                    return GetMemberName(lambda);
                case MemberExpression member:
                    return member?.Member?.Name ?? string.Empty;
                case MethodCallExpression methodCall:
                    return methodCall?.Method?.Name ?? string.Empty;
                case UnaryExpression unary:
                    return GetMemberName(unary);
                default:
                    throw new ArgumentException("Could not evaluate expression");
            }
        }

        public static string GetMemberName(UnaryExpression expression)
        {
            switch (expression.Operand)
            {
                case MethodCallExpression method:
                    return method?.Method?.Name ?? string.Empty;
                default:
                    return ((MemberExpression)expression.Operand)?.Member?.Name ?? string.Empty;
            }
        }

        public static string GetMemberName(LambdaExpression expression) =>
            ((MemberExpression)expression.Body)?.Member?.Name ?? string.Empty;

        public static IEnumerable<Type> GetClassesWithAttribute<TAttribute>(this Assembly assembly) =>
            assembly.GetTypes().Where(_ => _.GetCustomAttribute(typeof(TAttribute), true) != null);

        public static ValidatableObject<string> AsValidatable(this string target) =>
            new ValidatableObject<string> { Value = target };

        public static ValidatableObject<int> AsValidatable(this int target) =>
            new ValidatableObject<int> { Value = target };

        public static string ToFormDataString<T>(this T target) where T : class
        {
            var builder = new StringBuilder();
            foreach(var prop in typeof(T).GetProperties().OrderBy(_ => _.Name))
            {
                var value = prop.GetValue(target);
                if(value != null)
                {
                    builder.Append($"{prop.Name}={value}&");
                }
            }
            var result = builder.ToString();
            return result?.Length > 1 ? result.Substring(0, result.Length - 1) : string.Empty;
        }
    }
}
