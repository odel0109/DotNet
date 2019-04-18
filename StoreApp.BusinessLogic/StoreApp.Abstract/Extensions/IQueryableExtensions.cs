using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Linq.Expressions;

namespace StoreApp.Abstract.Extensions
{
    /// <summary>
    /// Set of parameters for queries. Contains conditions for Skip, Take, Where and OrderBy methods
    /// </summary>
    public class QueryParameterSet<TEntityType>
    {
        /// <summary>
        /// Count of rows which will be skiped
        /// </summary>
        public int? SkipCount { get; set; }

        /// <summary>
        /// Count of rows which will be taken
        /// </summary>
        public int? TakeCount { get; set; }

        /// <summary>
        /// Criteria applied to the query
        /// </summary>
        public Expression<Func<TEntityType, bool>> Criteria { get; set; }

        /// <summary>
        /// Ordering of rows. Example "ProductID"/Product.CategoryID
        /// </summary>
        public string OrderByPropertyName { get; set; }

        /// <summary>
        /// Should be set when ordering should be by descending
        /// </summary>
        public bool? OrderByDesc { get; set; }
    }

    public static class IQueryableExtensions
    {
        /// <summary>
        /// Applies parameters to IQueryable collection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="set"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ApplyCriteria<TEntity>(this IQueryable<TEntity> set, QueryParameterSet<TEntity> parameter)
        {
            var result = set;

            //if parameter set is null -> all collection should be returned
            if (parameter != null)
            {
                //if criteria exists -> where condition is applied
                if (parameter.Criteria != null)
                    result = result.Where(parameter.Criteria);

                //if OrderBy condition exists -> OrderBy condtion is applied
                if(parameter.OrderByPropertyName != null && !parameter.OrderByPropertyName.Equals(string.Empty))
                {
                    string method = "OrderBy";

                    if (parameter.OrderByDesc.HasValue && parameter.OrderByDesc.Value)
                        method += "Descending";

                    result = result.OrderByMemberUsing(parameter.OrderByPropertyName, method);
                }

                //if first X records should be skiped
                if (parameter.SkipCount.HasValue)
                    result = result.Skip(parameter.SkipCount.Value);

                //if only Y records should be taken
                if (parameter.TakeCount.HasValue)
                    result = result.Take(parameter.TakeCount.Value);
            }

            return result;
        }

        private static IOrderedQueryable<TEntity> OrderByMemberUsing<TEntity>(this IQueryable<TEntity> source, string memberPath, string method)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "item");
            var member = memberPath.Split('.')
                .Aggregate((Expression)parameter, Expression.PropertyOrField);
            var keySelector = Expression.Lambda(member, parameter);
            var methodCall = Expression.Call(
                typeof(Queryable), method, new[] { parameter.Type, member.Type },
                source.Expression, Expression.Quote(keySelector));
            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery(methodCall);
        }
    }
}
