using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Extensions
{
    public static class StringExtensions
    {

        public static string ConvertAllLettersToNumbers(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return value;
            string result = "";

            for (int i = 0; i < value.Length; i++)
            {
                char tempChar = value[i];
                if (char.IsLetter(value[i]))
                {
                    result += char.ToUpper(value[i]) - 64;
                    continue;
                }

                result += value[i];
            }

            return result;
        }
        public static string SplitByCamelCase(this string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        public static string ToCETString(this DateTime value)
        {
            string result = "";
            string format = "dd.MM.yyyy HH:mm";
            result = value.ToString(format);
            return result;
        }

        public static string ToShortCETString(this DateTime value)
        {
            string result = "";
            string format = "dd.MM.yyyy";
            result = value.ToString(format);
            return result;
        }

    }


}
