using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using DocumentFormat.OpenXml.Vml;
using OpenXMLHelpers.Utilities.Interface;

namespace OpenXMLHelpers.Utilities.Implementation
{
    public class Converter: IConverter
    {
        public ImagePart ConvertImageFromShape(WordprocessingDocument sourceDoc, Run sourceRun, ImageData imageData)
        {
            ImagePart p = sourceDoc.MainDocumentPart.GetPartById(imageData.RelationshipId) as ImagePart;
            return p;
        }
        public DateTime ConvertStringToDate(string source)
        {
            DateTime returnValue = new DateTime();
            if (!DateTime.TryParse(source, out returnValue))
            {
                throw new InvalidDataException("PARSER_CONVERT_STRING_TO_DATE: Napaka formata dokumenta! Format datuma nije dobar. (Valjani format: 01.01.2001)");
            }
            return returnValue;
        }
    }
}
