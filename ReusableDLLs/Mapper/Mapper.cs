using Mapper.Enumerators;
using Mapper.Mappers;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class Mapper<DatabaseClass, DtoClass> : IDisposable where DatabaseClass : class, new() where DtoClass : class, new()
    {
        // Flag: Has Dispose already been called?
        private bool disposed = false;
        // Instantiate a SafeHandle instance.
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        private readonly BaseMapper<DatabaseClass, DtoClass> _mapper;

        public Mapper(MapperTypeEnum mapperType)
        {
            switch (mapperType)
            {
                case MapperTypeEnum.ConventionBasedMapper:
                    _mapper = new ConventionMapper<DatabaseClass, DtoClass>();
                    break;
                case MapperTypeEnum.SameNamesMapper:
                    _mapper = new SameNamesMapper<DatabaseClass, DtoClass>();
                    break;
                default:
                    break;
            }
        }

        public DtoClass ToDto(DatabaseClass origin)
        {
            var result = _mapper.ToDto(origin);

            return result;
        }
        public List<DtoClass> ToDtoList(List<DatabaseClass> databaseObjectList)
        {
            var result = _mapper.ToDtoList(databaseObjectList);
            return result;
        }

        public DatabaseClass ToDatabase(DtoClass origin)
        {
            var result = _mapper.ToDatabase(origin);

            return result;
        }
        public List<DatabaseClass> ToDatabaseList(List<DtoClass> dtoList)
        {
            var result = _mapper.ToDatabaseList(dtoList);

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

    }
}
