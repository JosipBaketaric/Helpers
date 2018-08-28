using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mapper
{
    internal class Utilities
    {
        public List<PropertyInfo> GetClassProperties<T>()
        {
            return typeof(T).GetProperties().ToList();
        }
        public List<string> GetClassPropertieNames<T>()
        {
            return typeof(T).GetProperties().Select(x => x.Name).ToList();
        }

        public void GetPropValue(object src, string propName, out object[] result)
        {
            result = null;
            var property = src.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);

            var propValue = src.GetType().GetProperty(propName).GetValue(src, null);

            if (propValue is IEnumerable<object>)
            {
                result = ((IEnumerable<object>)propValue).ToArray();
            }
            else
            {
                result = new object[] { propValue };
            }

        }

        public void SetObjectProperty(object src, string propName, object[] value)
        {
            PropertyInfo pi = src.GetType().GetProperty(propName);
            if (IsPropertyList(pi))
            {
                
                pi.SetValue(src, ConvertList(value.ToList(), pi.PropertyType, pi) );
                return;
            }


            src.GetType().InvokeMember(propName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
            Type.DefaultBinder, src, value);
        }

        public string SplitCamelCase(string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        public object[] ConvertObjectValueTo(object instance, object[] parameters, string convertMehod)
        {
            Type convertHolderType = instance.GetType();
            MethodInfo theMethod = convertHolderType.GetMethod(convertMehod);

            var result = theMethod.Invoke(instance, parameters);
            object[] resultArray = { result };
            return resultArray;
        }
        public object[] ConvertObjectArrayTo(object[] elements, string convertMehod)
        {
            var instance = elements[0];
            Type convertHolderType = elements[0].GetType();
            MethodInfo theMethod = convertHolderType.GetMethod(convertMehod);
            List<object> result = new List<object>();

            foreach (var element in elements)
            {
                result.Add(theMethod.Invoke(instance, new object[] {  }));

            }
             
            return result.ToArray();
        }


        public bool IsPropertyList(PropertyInfo property)
        {
            if (!property.PropertyType.IsGenericType)
            {
                return false;
            }

            string TypeName = property.PropertyType.GetGenericTypeDefinition().Name;
            if (TypeName == "List`1")
            {
                return true;
            }
            return false;
        }

        public bool IsParameterList(ParameterInfo parameter)
        {
            if (!parameter.ParameterType.IsGenericType)
            {
                return false;
            }

            string TypeName = parameter.ParameterType.GetGenericTypeDefinition().Name;
            if (TypeName == "List`1")
            {
                return true;
            }
            return false;
        }

        public static object ConvertList(List<object> value, Type type, PropertyInfo pi)
        {
            //Create list
            var listType = typeof(List<>);
            var instance = (IList)Activator.CreateInstance(type);
            var containedType = type.GenericTypeArguments.First();

            //Assign values
            foreach (var item in value)
            {
                instance.Add(Convert.ChangeType(item, containedType));
            }

            return instance;
        }



    }
}
