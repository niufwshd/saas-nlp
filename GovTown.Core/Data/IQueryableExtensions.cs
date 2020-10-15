using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;

namespace GovTown.Core.Data
{
	public static class IQueryableExtensions
	{

		/// <summary>
		/// Instructs the repository to eager load entities that may be in the type's association path.
		/// </summary>
		/// <param name="query">A previously created query object which the expansion should be applied to.</param>
		/// <param name="path">
		/// The path of the child entities to eager load.
		/// Deeper paths can be specified by separating the path with dots.
		/// </param>
		/// <returns>A new query object to which the expansion was applied.</returns>
		public static IQueryable<T> Expand<T>(this IQueryable<T> query, string path) where T : BaseEntity
		{
			Guard.ArgumentNotNull(query, "query");
			Guard.ArgumentNotEmpty(path, "path");

			return query.Include(path);
		}

		/// <summary>
		/// Instructs the repository to eager load entities that may be in the type's association path.
		/// </summary>
		/// <param name="query">A previously created query object which the expansion should be applied to.</param>
		/// <param name="path">The path of the child entities to eager load.</param>
		/// <returns>A new query object to which the expansion was applied.</returns>
		public static IQueryable<T> Expand<T, TProperty>(this IQueryable<T> query, Expression<Func<T, TProperty>> path) where T : BaseEntity
		{
			Guard.ArgumentNotNull(query, "query");
			Guard.ArgumentNotNull(path, "path");

			return query.Include(path);
		}

        /// <summary>
        ///  返回PagedList<T>类型的IQueryable的扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="linq"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> linq, int pageIndex, int pageSize)
        {
            return new PagedList<T>(linq, pageIndex, pageSize);
        }
        /// <summary>
        /// 升序排列扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, String propertyName)
        {
            return InternalOrder<T>(source, propertyName, "OrderBy");
        }
        /// <summary>
        /// 降序排列扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, String propertyName)
        {
            return InternalOrder<T>(source, propertyName, "OrderByDescending");
        }
        /// <summary>
        /// 继续升序排列扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, String propertyName)
        {
            return InternalOrder<T>(source, propertyName, "ThenBy");
        }
        /// <summary>
        /// 继续降序排列扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, String propertyName)
        {
            return InternalOrder<T>(source, propertyName, "ThenByDescending");
        }
        /// <summary>
        /// 排序内部函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> InternalOrder<T>(IQueryable<T> source, String propertyName, String methodName)
        {
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "p");
            PropertyInfo property = type.GetProperty(propertyName);
            Expression expr = Expression.Property(arg, property);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), property.PropertyType);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            return ((IOrderedQueryable<T>)(typeof(Queryable).GetMethods().Single(
                p => String.Equals(p.Name, methodName, StringComparison.Ordinal)
                    && p.IsGenericMethodDefinition
                    && p.GetGenericArguments().Length == 2
                    && p.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.PropertyType)
                .Invoke(null, new Object[] { source, lambda })));
        }
    }
}
