﻿using Mapper.Attributes;
using Mapper.Enumerators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mapper
{
    internal class Utilities
    {
        private readonly MapperTypeEnum _mapperType;
        public Utilities(MapperTypeEnum mapperType)
        {
            _mapperType = mapperType;
        }

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
            if (src == null || src.GetType() == null || src.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance) == null
                || src.GetType().GetProperty(propName) == null || src.GetType().GetProperty(propName).GetValue(src, null) == null)
            {
                result = null;
                return;
            }

            result = null;
            var property = src.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);

            var propValue = src.GetType().GetProperty(propName).GetValue(src, null);

            if (propValue is IEnumerable<object>)
            {
                result = (propValue as IEnumerable<object>).ToArray();
            }
            else
            {
                result = new object[] { propValue };
            }

        }


        public void SetObjectProperty(ref object src, string propName, object[] value)
        {

            SetObjectPropertyLogic(ref src, src.GetType(), propName, value);

        }


        private void SetObjectPropertyLogic(ref object src, Type type, string propName, object[] value)
        {
            if (src == null || src.GetType() == null || src?.GetType().GetProperty(propName) == null)
            {
                return;
            }

            var targetType = IsNullableType(type) ? Nullable.GetUnderlyingType(type) : type;

            PropertyInfo pi = targetType.GetProperty(propName);

            if (IsPropertyList(pi) && pi != null)
            {
                var listConverted = ConvertList(value.ToList(), pi.PropertyType, pi);

                if (listConverted != null)
                {
                    pi.SetValue(src, listConverted);
                    return;
                }
            }

            PropertyInfo prop = targetType.GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(src, value[0], null);
            }

            //type.InvokeMember(propName,
            //BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
            //Type.DefaultBinder, src, value);
        }

        private bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public string SplitCamelCase(string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        public object[] ConvertObjectValueTo(object instance, object[] parameters, string convertMehod, Type propertyType)
        {
            //Type propertyType = Type.GetType(mappingAttr.Name);
            var mapToInstance = Activator.CreateInstance(propertyType);

            var instanceType = instance.GetType();
            var d1 = typeof(Mapper.Mapper<,>);
            Type makeme = null;
            if (convertMehod == "ToDto")
            {
                Type[] typeArgs = { instanceType, propertyType };
                makeme = d1.MakeGenericType(typeArgs);
            }
            else
            {
                Type[] typeArgs = { propertyType, instanceType };
                makeme = d1.MakeGenericType(typeArgs);
            }

            //TODO change
            Object[] args = { _mapperType };

            //create mapper object
            object o = Activator.CreateInstance(makeme, args);

            if (o == null)
            {
                return null;
            }

            //call method
            var theMethod = o.GetType().GetMethod(convertMehod);

            //Type convertHolderType = instance.GetType();
            //MethodInfo theMethod = convertHolderType.GetMethod(convertMehod);


            if (theMethod == null)
            {
                return null;
            }

            var result = theMethod.Invoke(o, new object[] { instance });
            object[] resultArray = { result };
            return resultArray;
        }
        public object[] ConvertObjectArrayTo(object[] elements, string convertMehod, Type propertyType)
        {
            //var instance = elements[0];
            Type convertHolderType = elements[0].GetType();
            //MethodInfo theMethod = convertHolderType.GetMethod(convertMehod);
            List<object> result = new List<object>();

            foreach (var element in elements)
            {
                //result.Add(theMethod.Invoke(element, new object[] { }));
                var tempResult = ConvertObjectValueTo(element, new object[] { }, convertMehod, propertyType)[0];
                result.Add(tempResult);
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

        public static bool IsObjectList(object src)
        {
            if (src != null && src.GetType().FullName.Contains("Collections."))
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

            var containedList = value.FirstOrDefault();

            if (containedList == null)
            {
                return instance;
            }

            //Only one element in array
            if ((containedList as IList) == null)
            {
                instance.Add(Convert.ChangeType(containedList, containedType));
                return instance;
            }

            //Assign values
            foreach (var item in (containedList as IList))
            {
                instance.Add(Convert.ChangeType(item, containedType));
            }

            return instance;
        }

        public static bool IsAny(IEnumerable data)
        {
            if (data == null)
            {
                return false;
            }

            foreach (var item in data)
            {
                return true;
            }
            return false;
        }





    }
}
