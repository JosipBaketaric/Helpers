using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum res)
        {
            string description = res
                .GetType()
                .GetMember(res.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;

            return description;
        }

    }
}
