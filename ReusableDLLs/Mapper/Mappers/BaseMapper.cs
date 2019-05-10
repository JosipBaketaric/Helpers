using Mapper.Attributes;
using Mapper.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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








        public DtoClass ToDto(DatabaseClass sourceObject, Dictionary<object, object> DtoMappingDictionary = null)
        {
            if (sourceObject == null)
            {
                return null;
            }

            object result = (DtoClass)Activator.CreateInstance(typeof(DtoClass), new object[] { });
            var resultObjectProperties = _utilities.GetClassProperties<DtoClass>();



            foreach (var property in resultObjectProperties)
            {
                string resultObjectPropName = ConvertDtoNameToDatabaseName(property.Name);

                // Handle custom mappings
                object targetKey = null;

                DtoMappingDictionary
                    .Select(x => x.Key)
                    .ToList()
                    .ForEach(x => {
                        var expBody = (x as Expression<Func<DtoClass, string>>);



                        if (expBody != null && expBody.Body != null)
                        {
                            var memberName = (expBody.Body as MemberExpression).Member.Name;

                            if(memberName == property.Name)
                            {
                                targetKey = x;
                            }
                        }
                    });


                if (targetKey != null && DtoMappingDictionary.TryGetValue(targetKey, out object mappingFunc))
                {
                    var propResult = (mappingFunc as Func<DatabaseClass, object>).Invoke(sourceObject);
                    _utilities.SetObjectProperty(ref result, property.Name, new object[] { propResult });
                    continue;
                }





                // Handle standard logic
                _utilities.GetPropValue(sourceObject, resultObjectPropName, out object[] value);

                if (value == null || value.Length == 0)
                    continue;


                if (value[0].GetType().IsClass && !value[0].GetType().FullName.StartsWith("System."))
                {

                    if (value != null && !_utilities.IsPropertyList(property))
                    {

                        value = _utilities.ConvertObjectValueTo(value[0], new object[] { }, "ToDto", property.PropertyType);

                    }
                    if (value != null && _utilities.IsPropertyList(property))
                    {
                        value = _utilities.ConvertObjectArrayTo(value, "ToDto", property.PropertyType.GetGenericArguments()[0]);
                    }
                }

                _utilities.SetObjectProperty(ref result, property.Name, value);

            }

            return result as DtoClass;
        }
        public List<DtoClass> ToDtoList(List<DatabaseClass> sourceObjectList, Dictionary<object, object> DtoMappingDictionary = null)
        {
            List<DtoClass> result = new List<DtoClass>();

            if (sourceObjectList == null || sourceObjectList.Count == 0)
            {
                return result;
            }

            for (int i = 0; i < sourceObjectList.Count; i++)
            {
                result.Add(ToDto(sourceObjectList.ElementAt(i), DtoMappingDictionary));
            }

            return result;
        }







        public DatabaseClass ToDatabase(DtoClass sourceObject, Dictionary<object, object> DatabaseMappingDictionary = null)
        {
            if (sourceObject == null)
            {
                return null;
            }

            object result = (DatabaseClass)Activator.CreateInstance(typeof(DatabaseClass), new object[] { });
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

                _utilities.SetObjectProperty(ref result, property.Name, value);
            }

            return result as DatabaseClass;
        }

        public List<DatabaseClass> ToDatabaseList(List<DtoClass> dtoList, Dictionary<object, object> DatabaseMappingDictionary = null)
        {
            List<DatabaseClass> result = new List<DatabaseClass>();

            if (dtoList == null || dtoList.Count == 0)
            {
                return result;
            }

            for (int i = 0; i < dtoList.Count; i++)
            {
                result.Add(ToDatabase(dtoList.ElementAt(i), DatabaseMappingDictionary));
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
