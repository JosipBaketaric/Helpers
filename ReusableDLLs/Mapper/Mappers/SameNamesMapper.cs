namespace Mapper.Mappers
{
    internal class SameNamesMapper<DatabaseClass, DtoClass> : BaseMapper<DatabaseClass, DtoClass>
        where DatabaseClass : class
        where DtoClass : class
    {
        public SameNamesMapper(): base(new Utilities(Enumerators.MapperTypeEnum.SameNamesMapper))
        {
        }



        internal override string ConvertDtoNameToDatabaseName(string name)
        {
            return name;
        }
        internal override string ConvertDatabaseNameToDtoName(string name)
        {
            return name;
        }



    }
}
