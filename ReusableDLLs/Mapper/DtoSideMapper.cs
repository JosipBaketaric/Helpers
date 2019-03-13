using Mapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    internal class DtoSideMapper<DatabaseClass,DtoClass> 
        where DatabaseClass : class 
        where DtoClass : class        
    {

        public virtual DatabaseClass ToDatabase()
        {
            var mapper = new ConventionMapper<DatabaseClass, DtoClass>();
            var result = mapper.ToDatabase(this as DtoClass);

            return result;
        }
        public virtual List<DatabaseClass> ToDatabaseList(List<DtoClass> dtoList)
        {
            var mapper = new ConventionMapper<DatabaseClass, DtoClass>();
            var result = mapper.ToDatabaseList(dtoList);

            return result;
        }






    }
}
