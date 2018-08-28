using OpenXMLHelpers.Utilities.Interface;
using System.Collections.Generic;

namespace OpenXMLHelpers.Utilities.Implementation
{
    public class ParserUtilitiesBuilder : IParserUtilitiesBuilder
    {
        public IParserUtilities ParserUtilities { get; set; }
        public IConverter Converter { get; set; }
        public IFinder Finder { get; set; }
        public IValidator Validator { get; set; }


        public void BuildParserUtilities(List<string> ALLOWED_STYLES, string CLEN_NASLOV, string ODSTAVEK_NASLOV, string ALINEJA_NASLOV, string CRTA, 
            IValidator validator, IConverter converter, IFinder finder)
        {
            this.ParserUtilities = new ParserUtilities(ALLOWED_STYLES, CLEN_NASLOV, ODSTAVEK_NASLOV, ALINEJA_NASLOV, CRTA);
            this.ParserUtilities.Converter = converter;
            this.ParserUtilities.Finder = finder;
            this.ParserUtilities.Validator = validator;
        }

        public void BuildConverter()
        {
            this.Converter = new Converter();
        }

        public void BuildFinder(IValidator validator, string CLEN_NASLOV, string ODSTAVEK_NASLOV, string ALINEJA_NASLOV, string CRTA)
        {
            this.Finder = new Finder();
            this.Finder.Validator = validator;
            this.Finder.CLEN_NASLOV = CLEN_NASLOV;
            this.Finder.ODSTAVEK_NASLOV = ODSTAVEK_NASLOV;
            this.Finder.ALINEJA_NASLOV = ALINEJA_NASLOV;
            this.Finder.CRTA = CRTA;
        }



        public void BuildValidator(List<string> ALLOWED_STYLES)
        {
            this.Validator = new Validator();
            this.Validator.ALLOWED_STYLES = ALLOWED_STYLES;
        }

    }
}
