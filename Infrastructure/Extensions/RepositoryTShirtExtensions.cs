using Domain.Entities;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;


namespace Infrastructure.Extensions
{
    public static class RepositoryTShirtExtensions
    {
        public static IQueryable<TShirt> Sort(this IQueryable<TShirt> tshirts, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return tshirts.OrderBy(t => t.Name);
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(TShirt).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderByQueryString.ToString().TrimEnd(',', ' ');
            
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return tshirts.OrderBy(ts => ts.Name);
            }

            return tshirts.OrderBy(orderQuery);
        }

        public static IQueryable<TShirt> Search(this IQueryable<TShirt> tshirts, string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
            {
                return tshirts;
            }

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return tshirts.Where(ts => ts.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}