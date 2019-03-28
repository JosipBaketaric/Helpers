using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Interface
{
    public interface IDatabaseSideMapper<DatabaseClass, DtoClass> where DatabaseClass : class, new() where DtoClass : class, new()
    {
        DatabaseClass ToDatabase(DtoClass origin);
        IEnumerable<DatabaseClass> ToDatabaseList(IEnumerable<DtoClass> dtoList);
        List<DatabaseClass> ToDatabaseList(List<DtoClass> dtoList);
    }
}
