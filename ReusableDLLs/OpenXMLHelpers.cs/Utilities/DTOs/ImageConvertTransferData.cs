using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Packaging;

namespace OpenXMLHelpers.Utilities.DTOs
{
    public class ImageConvertTransferData
    {
        public Run Run { get; set; }
        public ImageData ImageData { get; set; }
        public ImagePart ImagePart { get; set; }
    }
}
