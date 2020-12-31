using Domain.Entities;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Domain.RequestFeatures;


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

            var orderQuery = orderByQueryString.TrimEnd(',', ' ');
            
            return string.IsNullOrWhiteSpace(orderQuery) ? tshirts.OrderBy(ts => ts.Name) : tshirts.OrderBy(orderQuery);
        }

        public static IQueryable<TShirt> Filter(this IQueryable<TShirt> tshirts, TshirtParameters tshirtParameters)
        {
            return tshirts.Where(c =>
                (string.IsNullOrWhiteSpace(tshirtParameters.Gender) || c.Gender.Name == tshirtParameters.Gender) &&
                (string.IsNullOrWhiteSpace(tshirtParameters.Category) || c.Category.Name == tshirtParameters.Category) &&
                (string.IsNullOrWhiteSpace(tshirtParameters.Author) || c.User.Email == tshirtParameters.Author));
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