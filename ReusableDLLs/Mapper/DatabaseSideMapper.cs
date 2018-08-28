using Mapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class DatabaseSideMapper<DatabaseClass,DtoClass> 
        where DatabaseClass : class 
        where DtoClass : class        
    {


        public virtual DtoClass ToDto()
        {
            var mapper = new ConventionMapper<DatabaseClass, DtoClass>();
            var result = mapper.ToDto(this as DatabaseClass);

            return result;
        }
        public virtual List<DtoClass> ToDtoList(List<DatabaseClass> databaseObjectList)
        {
            var mapper = new ConventionMapper<DatabaseClass, DtoClass>();

            var result = mapper.ToDtoList(databaseObjectList);

            return result;
        }


    }
}
