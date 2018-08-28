using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Vml;

namespace OpenXMLHelpers.Utilities.Interface
{
    public interface IConverter
    {
        ImagePart ConvertImageFromShape(WordprocessingDocument sourceDoc, Run sourceRun, ImageData imageData);
        DateTime ConvertStringToDate(string source);
    }
}
