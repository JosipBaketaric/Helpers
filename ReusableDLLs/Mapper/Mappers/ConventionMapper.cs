using Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Mapper.Mappers
{
    internal class ConventionMapper<DatabaseClass, DtoClass> : BaseMapper<DatabaseClass, DtoClass>
        where DatabaseClass : class
        where DtoClass : class
    {
        public ConventionMapper() :base(new Utilities(Enumerators.MapperTypeEnum.ConventionBasedMapper))
        {
        }



        internal override string ConvertDtoNameToDatabaseName(string name)
        {
            //TestFkSomethingName => TEST_FK_SOMETHING_NAME
            string splitName = _utilities.SplitCamelCase(name);
            string result = splitName.Replace(" ", "_").ToUpper();
            return result;
        }
        internal override string ConvertDatabaseNameToDtoName(string name)
        {
            //TEST_FK_SOMETHING_NAME => TestFkSomethingName
            name = name.ToLower();
            List<string> splitName = name.Split('_').ToList();
            splitName = splitName.Select(x =>  x = x.First().ToString().ToUpper() + x.Substring(1)).ToList();

            string result = string.Join("", splitName);
            return result;
        }



    }
}
