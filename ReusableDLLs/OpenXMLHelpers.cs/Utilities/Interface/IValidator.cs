using System.Collections.Generic;
using DocumentFormat.OpenXml;

namespace OpenXMLHelpers.Utilities.Interface
{
    public interface IValidator
    {
        List<string> ALLOWED_STYLES { get; set; }

        void ValidateResultCount(OpenXmlElementList source, string style, bool isZeroCountValid, bool isMultipleCountValid, int dashIndex, int styleCount);
        void ValidateResultCountBetween(OpenXmlElementList source, string style, bool isZeroCountValid, bool isMultipleCountValid, int lowerLimit, int upperLimit, int dashIndex, int styleCount);
        void ValidateResultNullOrEmpty(string result, string style);
        void ValidateDocumentStyles(List<string> style);
        bool IsCurrentSomethingOf(OpenXmlElementList source, int currentPositionOfI, List<string> something);
        bool IsXmlElementValidParagraphWithValidProperties(OpenXmlElement element);
        bool IsCurrentSomething(OpenXmlElementList source, int currentPositionOfI, string something);

    }
}
