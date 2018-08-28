using System.Collections.Generic;
using OpenXMLHelpers.Utilities.Interface;

namespace OpenXMLHelpers.Utilities.Implementation
{
    public class ParserUtilitiesConstructor : IParserUtilitiesConstructor
    {
        public void Construct(IParserUtilitiesBuilder parserUtilitiesBuilder, List<string> ALLOWED_STYLES, string CLEN_NASLOV, string ODSTAVEK_NASLOV, string ALINEJA_NASLOV, string CRTA)
        {
            parserUtilitiesBuilder.BuildConverter();
            parserUtilitiesBuilder.BuildValidator(ALLOWED_STYLES);
            parserUtilitiesBuilder.BuildFinder(parserUtilitiesBuilder.Validator, CLEN_NASLOV, ODSTAVEK_NASLOV, ALINEJA_NASLOV, CRTA);

            parserUtilitiesBuilder.BuildParserUtilities(ALLOWED_STYLES, CLEN_NASLOV, ODSTAVEK_NASLOV, ALINEJA_NASLOV, CRTA, parserUtilitiesBuilder.Validator, parserUtilitiesBuilder.Converter, parserUtilitiesBuilder.Finder);

        }
    }
}
