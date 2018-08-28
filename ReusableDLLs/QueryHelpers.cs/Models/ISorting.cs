using QueryHelpers.cs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryHelpers.cs.Models
{
    public interface ISorting
    {
        SortOrderEnum SortingOrder { get; set; }
        string OrderByColumnName { get; set; }
    }
}
