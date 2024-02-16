using System.Linq.Expressions;
using System.Reflection;
#nullable disable
namespace Core.Utils
{
    public static class OrderingUtils
    {
        /// <summary>
        /// Check if is Nullable
        /// </summary>
        public static bool IsNullable(Type type)
        {
            return type == null || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Check if is IEnumerable<>
        /// </summary>
        public static bool IsGenericEnumerable(Type type)
        {
            var genArgs = type.GetGenericArguments();

            if (genArgs.Length == 1 && typeof(IEnumerable<>).MakeGenericType(genArgs).IsAssignableFrom(type))
                return true;
            else
                return type.BaseType != null && IsGenericEnumerable(type.BaseType);
        }

        public static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderBy").Single(method => method.GetParameters().Length == 2);

        public static readonly MethodInfo OrderByDescending = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderByDescending").Single(method => method.GetParameters().Length == 2);

        public static IQueryable<TSource> CallOrderBy<TSource>(IQueryable<TSource> source, string propertyName, bool ascending)
        {
            var orderMethod = ascending ? OrderByMethod : OrderByDescending;

            var parameter = Expression.Parameter(typeof(TSource));

            Expression orderByProperty = Expression.Property(parameter, propertyName);

            var lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            var genericMethod = orderMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });

            var ret = genericMethod.Invoke(null, new object[] { source, lambda });

            return (IQueryable<TSource>)ret;
        }

    }
}
