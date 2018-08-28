using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXMLHelpers.Utilities.Interface;

namespace OpenXMLHelpers.Utilities.Implementation
{
    public class ParserUtilities : IParserUtilities
    {
        private List<string> ALLOWED_STYLES;
        private string CLEN_NASLOV;
        private string ODSTAVEK_NASLOV;
        private string ALINEJA_NASLOV;
        private string CRTA;
        public IConverter Converter { get; set; }
        public IFinder Finder { get; set; }
        public IValidator Validator { get; set; }

        public ParserUtilities(List<string> ALLOWED_STYLES, string CLEN_NASLOV, string ODSTAVEK_NASLOV, string ALINEJA_NASLOV, string CRTA)
        {
            this.ALLOWED_STYLES = ALLOWED_STYLES;
            this.CLEN_NASLOV = CLEN_NASLOV;
            this.ODSTAVEK_NASLOV = ODSTAVEK_NASLOV;
            this.ALINEJA_NASLOV = ALINEJA_NASLOV;
            this.CRTA = CRTA;
        }


        //TODO: MOVE GetImageInHtmlBase64Format IN Converter CLASS AND RENAME IN ConvertImageInHtmlBase64Format
        public string GetImageInHtmlBase64Format(WordprocessingDocument wordprocessingDocument, Paragraph paragraph)
        {
            var image = Finder.FindImage(wordprocessingDocument, paragraph);
            return Finder.GetImageInHtmlBase64Format(wordprocessingDocument, paragraph, image.ImagePart);
        }

        public ImagePart FindImage(WordprocessingDocument wordproccessingDocument, Paragraph par)
        {
            var result = Finder.FindImage(wordproccessingDocument, par);
            if (result.ImagePart != null)
                return result.ImagePart;
            return Converter.ConvertImageFromShape(wordproccessingDocument, result.Run, result.ImageData);

        }


       
    }
}