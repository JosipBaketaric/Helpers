using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Interface
{
    public interface IDomainSideMapper<DatabaseClass, DtoClass> where DatabaseClass : class, new() where DtoClass : class, new()
    {
        DtoClass ToDto(DatabaseClass origin);

        IEnumerable<DtoClass> ToDtoList(IEnumerable<DatabaseClass> databaseObjectList);

        List<DtoClass> ToDtoList(List<DatabaseClass> databaseObjectList);
    }
}
