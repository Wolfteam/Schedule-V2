using AutoMapper;
using AutoMapper.QueryableExtensions;
using Schedule.Shared.Interfaces.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Schedule.Shared.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "OrderBy", propertyName, comparer);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "OrderByDescending", propertyName, comparer);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "ThenBy", propertyName, comparer);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "ThenByDescending", propertyName, comparer);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IPaginatedRequestDto dto)
        {
            return query.Paginate(dto.Take, dto.Page, dto.OrderBy, dto.OrderByAsc);
        }

        public static IQueryable<TProjectTo> Paginate<T, TProjectTo>(
            this IQueryable<T> query,
            IPaginatedRequestDto request,
            IConfigurationProvider configurationProvider)
            where TProjectTo : class, new()
        {
            return query.Paginate<T, TProjectTo>(request.Take, request.Page, configurationProvider, request.OrderBy, request.OrderByAsc);
        }

        public static IQueryable<TProjectTo> Paginate<T, TProjectTo>(
            this IQueryable<T> query,
            IPaginatedRequestDto request,
            IPaginatedResponseDto response,
            IConfigurationProvider configurationProvider)
            where TProjectTo : class, new()
        {
            response.Take = request.Take;
            response.CurrentPage = request.Page;
            response.TotalRecords = query.Count();
            return query.Paginate<T, TProjectTo>(request.Take, request.Page, configurationProvider, request.OrderBy, request.OrderByAsc);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int take, int page)
        {
            return query.Paginate(take, page, null, true);
        }

        public static IQueryable<T> Paginate<T>(
            this IQueryable<T> query,
            int take,
            int page,
            string orderByPropertyName,
            bool orderByAsc)
        {
            int skip = take * (page - 1);
            return string.IsNullOrEmpty(orderByPropertyName)
                ? query.Skip(skip).Take(take)
                : orderByAsc
                    ? query.OrderBy(orderByPropertyName).Skip(skip).Take(take)
                    : query.OrderByDescending(orderByPropertyName).Skip(skip).Take(take);
        }

        public static IQueryable<TProjectTo> Paginate<T, TProjectTo>(
            this IQueryable<T> query,
            int take,
            int page,
            IConfigurationProvider configurationProvider,
            string orderByPropertyName = null,
            bool orderByAsc = true)
            where TProjectTo : class, new()
        {
            return query.Paginate(take, page, orderByPropertyName, orderByAsc).ProjectTo<TProjectTo>(configurationProvider);
        }

        /// <summary>
        /// Builds the Queryable functions using a TSource property name.
        /// </summary>
        public static IOrderedQueryable<T> CallOrderedQueryable<T>(this IQueryable<T> query, string methodName, string propertyName,
            IComparer<object> comparer = null)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            return comparer != null
                ? (IOrderedQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(T), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param),
                        Expression.Constant(comparer)
                    )
                )
                : (IOrderedQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(T), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param)
                    )
                );
        }
    }
}
