using System;
using System.Linq;
using System.Reflection;
using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.Helpers
{
    public static class Mapper
    {
        public static TTarget Map<TSource, TTarget>(this TSource source)
        {
            var result = Activator.CreateInstance<TTarget>();
            var targetType = typeof(TTarget);
            foreach (var pi in typeof(TSource).GetRuntimeProperties().Where(_ => _.GetMethod?.IsPublic ?? false))
            {
                var targetPi = targetType.GetRuntimeProperty(pi.Name);
                if ((targetPi?.SetMethod?.IsPublic ?? false) && (pi.PropertyType == targetPi.PropertyType || typeof(IValidity).IsAssignableFrom(pi.PropertyType)))
                {
                    if (typeof(IValidity).IsAssignableFrom(pi.PropertyType))
                    {
                        targetPi.SetValue(result, ((IValidity)pi.GetValue(source)).GetValue());
                    }
                    else
                    {
                        targetPi.SetValue(result, pi.GetValue(source));
                    }
                }
            }
            return result;
        }
    }
}
