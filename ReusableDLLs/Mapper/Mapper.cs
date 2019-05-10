using Mapper.Enumerators;
using Mapper.Interface;
using Mapper.Mappers;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class Mapper<DatabaseClass, DtoClass> : IDisposable, IDatabaseSideMapper<DatabaseClass, DtoClass>, IDomainSideMapper<DatabaseClass, DtoClass> 
        where DatabaseClass : class, new() where DtoClass : class, new() 
    {
        // Flag: Has Dispose already been called?
        private bool disposed = false;
        // Instantiate a SafeHandle instance.
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        private readonly Dictionary<object, object> DtoMappingDictionary = new Dictionary<object, object>();
        private readonly Dictionary<object, object> DatabaseMappingDictionary = new Dictionary<object, object>();

        private readonly BaseMapper<DatabaseClass, DtoClass> _mapper;

        #region Constructors
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
        #endregion

        #region Methods
        // Custom mappings for properties that override default mapping
        public void CreateMapDto<T>(Expression<Func<DtoClass, T>> selectedProperty, Func<DatabaseClass, T> mappingFunction)
        {
            DtoMappingDictionary.Add(selectedProperty, mappingFunction);
        }

        public void CreateMapDatabase<T>(Expression<Func<DatabaseClass, T>> selectedProperty, Func<DtoClass, T> mappingFunction)
        {
            DatabaseMappingDictionary.Add(selectedProperty, mappingFunction);
        }





        public DtoClass ToDto(DatabaseClass origin)
        {
            var result = _mapper.ToDto(origin, DtoMappingDictionary);
            return result;
        }

        public IEnumerable<DtoClass> ToDtoList(IEnumerable<DatabaseClass> databaseObjectList)
        {
            var result = _mapper.ToDtoList(databaseObjectList.ToList(), DtoMappingDictionary);
            return result;
        }

        public List<DtoClass> ToDtoList(List<DatabaseClass> databaseObjectList)
        {
            var result = _mapper.ToDtoList(databaseObjectList, DtoMappingDictionary);
            return result;
        }

        public DatabaseClass ToDatabase(DtoClass origin)
        {
            var result = _mapper.ToDatabase(origin, DatabaseMappingDictionary);
            return result;
        }

        public IEnumerable<DatabaseClass> ToDatabaseList(IEnumerable<DtoClass> dtoList)
        {
            var result = _mapper.ToDatabaseList(dtoList.ToList(), DatabaseMappingDictionary);

            return result;
        }
        public List<DatabaseClass> ToDatabaseList(List<DtoClass> dtoList)
        {
            var result = _mapper.ToDatabaseList(dtoList, DatabaseMappingDictionary);
            return result;
        }
        #endregion



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
