using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXMLHelpers.Utilities.DTOs;

namespace OpenXMLHelpers.Utilities.Interface
{
    public interface IFinder
    {
        IValidator Validator { get; set; }
        string CLEN_NASLOV { get; set; }
        string ODSTAVEK_NASLOV { get; set; }
        string ALINEJA_NASLOV { get; set; }
        string CRTA { get; set; }


        KeyValuePair<int, string> FindFirst(OpenXmlElementList source, string id);
        Dictionary<int, string> FindMany(OpenXmlElementList source, string id, int dashIndex = -1);
        KeyValuePair<int, string> FindFirstBetween(OpenXmlElementList source, string id, int lowerLimit, int upperLimit);
        KeyValuePair<int, string> FindFirstOfBetween(OpenXmlElementList source, List<string> idList, int lowerLimit, int upperLimit);
        Dictionary<int, string> FindManyBetween(OpenXmlElementList source, string id, int lowerLimit, int upperLimit, int dashIndex = -1);
        Dictionary<int, string> FindManyBetween(OpenXmlElementList source, List<string> idList, int lowerLimit, int upperLimit, int dashIndex = -1);
        int FindStartIndexOfNextSection(OpenXmlElementList source, int startIndex, int dashIndex);
        int FindElementEndIndex(OpenXmlElementList source, int startIndex, List<string> styleBreakerList, int dashIndex);
        ImageConvertTransferData FindImage(WordprocessingDocument wordproccessingDocument, Paragraph par);
        string GetParagraphStyle(OpenXmlElementList source, int currentPositionOfI);
        string GetStyleFromParagraph(OpenXmlElement el);
        string GetImageInHtmlBase64Format(WordprocessingDocument wordprocessingDocument, Paragraph paragraph, ImagePart image);
        string GetIndentionTextFromParagraph(WordprocessingDocument doc, int elementPosition);
        List<string> GetAllDocumentStyles(OpenXmlElementList source, int dashIndex);

        int CountStyle(OpenXmlElementList source, string style, int dashIndex);
        int CountStyleBetween(OpenXmlElementList source, string style, int dashIndex, int lowerLimit, int upperLimit);
    }
}
