﻿using Mapper.Attributes;
using Mapper.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Mappers
{
    internal class BaseMapper<DatabaseClass, DtoClass>
           where DatabaseClass : class
           where DtoClass : class
    {
        protected Utilities _utilities;
        public BaseMapper(Utilities utilities)
        {
            _utilities = utilities;
        }
        public DtoClass ToDto(DatabaseClass sourceObject)
        {
            if (sourceObject == null)
            {
                return null;
            }

            DtoClass result = (DtoClass)Activator.CreateInstance(typeof(DtoClass), new object[] { });
            var resultObjectProperties = _utilities.GetClassProperties<DtoClass>();

            foreach (var property in resultObjectProperties)
            {
                string resultObjectPropName = ConvertDtoNameToDatabaseName(property.Name);


                _utilities.GetPropValue(sourceObject, resultObjectPropName, out object[] value);

                if (value == null || value.Length == 0)
                    continue;

                if (value[0].GetType().IsClass && !value[0].GetType().FullName.StartsWith("System."))
                {
                    //var mappingAttr = property
                    //    .GetCustomAttributes(true)
                    //    .OfType<MappingClassAttribute>()
                    //    .FirstOrDefault();

                    //if (mappingAttr == null)
                        //continue;

                    if (value != null && value.Count() == 1)
                    {
                        value = _utilities.ConvertObjectValueTo(value[0], new object[] { }, "ToDto", property.GetType());
                    }
                    if (value != null && value.Count() > 1)
                    {
                        value = _utilities.ConvertObjectArrayTo(value, "ToDto", property.PropertyType.GetGenericArguments()[0]);
                    }
                }

                _utilities.SetObjectProperty(result, property.Name, value);

            }

            return result;
        }
        public List<DtoClass> ToDtoList(List<DatabaseClass> sourceObjectList)
        {
            List<DtoClass> result = new List<DtoClass>();

            if (sourceObjectList == null || sourceObjectList.Count == 0)
            {
                return result;
            }

            for (int i = 0; i < sourceObjectList.Count; i++)
            {
                result.Add(ToDto(sourceObjectList.ElementAt(i)));
            }

            return result;
        }

        public DatabaseClass ToDatabase(DtoClass sourceObject)
        {
            if (sourceObject == null)
            {
                return null;
            }

            DatabaseClass result = (DatabaseClass)Activator.CreateInstance(typeof(DatabaseClass), new object[] { });
            var resultObjectProperties = _utilities.GetClassProperties<DatabaseClass>();

            foreach (var property in resultObjectProperties)
            {
                string resultObjectPropName = ConvertDatabaseNameToDtoName(property.Name);
                _utilities.GetPropValue(sourceObject, resultObjectPropName, out object[] value);

                if (value == null)
                    continue;

                if (value != null && value[0] != null && value[0].GetType().IsClass && !value[0].GetType().FullName.StartsWith("System."))
                {
                    //var mappingAttr = property
                    //   .GetCustomAttributes(true)
                    //   .OfType<MappingClassAttribute>()
                    //   .FirstOrDefault();

                    //if (mappingAttr == null)
                    //    continue;

                    if (value != null && value.Count() == 1)
                    {
                        value = _utilities.ConvertObjectValueTo(value[0], null, "ToDatabase", property.GetType());
                    }
                    if (value != null && value.Count() > 1)
                    {
                        value = _utilities.ConvertObjectArrayTo(value, "ToDatabase", property.PropertyType.GetGenericArguments()[0]);
                    }
                }

                _utilities.SetObjectProperty(result, property.Name, value);
            }

            return result;
        }


        public List<DatabaseClass> ToDatabaseList(List<DtoClass> dtoList)
        {
            List<DatabaseClass> result = new List<DatabaseClass>();

            if (dtoList == null || dtoList.Count == 0)
            {
                return result;
            }

            for (int i = 0; i < dtoList.Count; i++)
            {
                result.Add(ToDatabase(dtoList.ElementAt(i)));
            }

            return result;
        }



        internal virtual string ConvertDtoNameToDatabaseName(string name)
        {
            //TestFkSomethingName => TEST_FK_SOMETHING_NAME
            string splitName = _utilities.SplitCamelCase(name);
            string result = splitName.Replace(" ", "_").ToUpper();
            return result;
        }
        internal virtual string ConvertDatabaseNameToDtoName(string name)
        {
            //TEST_FK_SOMETHING_NAME => TestFkSomethingName
            name = name.ToLower();
            List<string> splitName = name.Split('_').ToList();
            splitName = splitName.Select(x => x = x.First().ToString().ToUpper() + x.Substring(1)).ToList();

            string result = string.Join("", splitName);
            return result;
        }



    }
}