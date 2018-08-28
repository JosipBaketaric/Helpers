using QueryHelpers.cs.Enums;
using QueryHelpers.cs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryHelpers.cs
{
    public static class QueryExtensions
    {
        public static IOrderedQueryable<T> SortBy<T, B>(this IQueryable<T> query, B filter) where B : ISorting
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(filter.OrderByColumnName, false);
            if (filter.SortingOrder == SortOrderEnum.Ascending)
            {
                return query.OrderBy(x => prop.GetValue(x));
            }
            else
            {
                return query.OrderByDescending(x => prop.GetValue(x));
            }

        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string columnName, SortOrderEnum sortOrder)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(columnName, false);
            if (sortOrder == SortOrderEnum.Ascending)
            {
                return query.ThenBy(x => prop.GetValue(x));
            }
            else
            {
                return query.ThenByDescending(x => prop.GetValue(x));
            }

        }
    }
}
