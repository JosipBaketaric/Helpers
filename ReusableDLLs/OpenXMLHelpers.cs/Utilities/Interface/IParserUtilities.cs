using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace OpenXMLHelpers.Utilities.Interface
{
    public interface IParserUtilities
    {
        IConverter Converter { get; set; }
        IFinder Finder { get; set; }
        IValidator Validator { get; set; }
        string GetImageInHtmlBase64Format(WordprocessingDocument wordprocessingDocument, Paragraph paragraph);
        ImagePart FindImage(WordprocessingDocument wordproccessingDocument, Paragraph par);

    }
}
