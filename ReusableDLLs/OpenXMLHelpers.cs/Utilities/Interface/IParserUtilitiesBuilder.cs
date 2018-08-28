using System.Collections.Generic;

namespace OpenXMLHelpers.Utilities.Interface
{
    public interface IParserUtilitiesBuilder
    {

        IParserUtilities ParserUtilities { get; set; }
        IConverter Converter { get; set; }
        IFinder Finder { get; set; }
        IValidator Validator { get; set; }


        void BuildParserUtilities(List<string> ALLOWED_STYLES, string CLEN_NASLOV, string ODSTAVEK_NASLOV, string ALINEJA_NASLOV, string CRTA,
            IValidator validator, IConverter converter, IFinder finder);

        void BuildConverter();

        void BuildFinder(IValidator validator, string CLEN_NASLOV, string ODSTAVEK_NASLOV, string ALINEJA_NASLOV, string CRTA);

        void BuildValidator(List<string> ALLOWED_STYLES);

    }
}
