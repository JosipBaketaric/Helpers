using System.Collections.Generic;

namespace OpenXMLHelpers.Utilities.Interface
{
    public interface IParserUtilitiesConstructor
    {
        void Construct(IParserUtilitiesBuilder parserUtilitiesBuilder, List<string> ALLOWED_STYLES, string CLEN_NASLOV, string ODSTAVEK_NASLOV, string ALINEJA_NASLOV, string CRTA);
    }
}
