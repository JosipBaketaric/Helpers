using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using DocumentFormat.OpenXml.Vml;
using OpenXmlPowerTools;
using OpenXMLHelpers.Utilities.Interface;
using OpenXMLHelpers.Utilities.DTOs;

namespace OpenXMLHelpers.Utilities.Implementation
{
    public class Finder : IFinder
    {
        public IValidator Validator { get; set; }

        public string CLEN_NASLOV { get; set; }
        public string ODSTAVEK_NASLOV { get; set; }
        public string ALINEJA_NASLOV { get; set; }
        public string CRTA { get; set; }


        public KeyValuePair<int, string> FindFirst(OpenXmlElementList source, string id)
        {
            var result = source
                    .Select((x, i) => new { Value = x, Index = i })
                    .Where(x => (x.Value is Paragraph))
                    .Where(x => (x.Value as Paragraph).ParagraphProperties != null)
                    .Where(x => (x.Value as Paragraph).ParagraphProperties.ParagraphStyleId != null)
                    .Where(x => (x.Value as Paragraph).ParagraphProperties?.ParagraphStyleId?.Val?.Value?.ToLower() == id.ToLower())
                    .ToList();

            if (result.Count == 0)
                return new KeyValuePair<int, string>(-1, "");

            KeyValuePair<int, string> returnValue = new KeyValuePair<int, string>(result.First().Index, result.First().Value.InnerText);

            return returnValue;
        }
        public Dictionary<int, string> FindMany(OpenXmlElementList source, string id, int dashIndex = -1)
        {
            if (dashIndex == -1)
                dashIndex = source.Count;

            var result = source
                     .Select((x, i) => new { Value = x, index = i })
                     .Where(x => x.index < dashIndex)
                     .Where(x => (x.Value is Paragraph))
                     .Where(x => (x.Value as Paragraph).ParagraphProperties != null)
                     .Where(x => (x.Value as Paragraph).ParagraphProperties.ParagraphStyleId != null)
                     .Where(x => (x.Value as Paragraph).ParagraphProperties?.ParagraphStyleId?.Val?.Value?.ToLower() == id.ToLower())
                     .ToList();

            Dictionary<int, string> returnDictionary = new Dictionary<int, string>();

            foreach (var item in result)
                returnDictionary.Add(item.index, item.Value.InnerText);

            return returnDictionary;
        }
        public KeyValuePair<int, string> FindFirstBetween(OpenXmlElementList source, string id, int lowerLimit, int upperLimit)
        {
            var result = source
                    .Select((x, i) => new { Value = x, Index = i })
                    .Where(x => (x.Value is Paragraph))
                    .Where(x => (x.Value as Paragraph).ParagraphProperties != null)
                    .Where(x => (x.Value as Paragraph).ParagraphProperties.ParagraphStyleId != null)
                    .Where(x => (x.Value as Paragraph).ParagraphProperties?.ParagraphStyleId?.Val?.Value?.ToLower() == id.ToLower())
                    .Where(x => x.Index >= lowerLimit)
                    .Where(x => x.Index <= upperLimit)
                    .ToList();

            if (result.Count == 0)
                return new KeyValuePair<int, string>(-1, "");

            KeyValuePair<int, string> returnValue = new KeyValuePair<int, string>(result.First().Index, result.First().Value.InnerText);

            return returnValue;
        }
        public KeyValuePair<int, string> FindFirstOfBetween(OpenXmlElementList source, List<string> idList, int lowerLimit, int upperLimit)
        {
            idList = idList.ConvertAll(x => x.ToLower()).ToList();
            var result = source
                    .Select((x, i) => new { Value = x, Index = i })
                    .Where(x => (x.Value is Paragraph))
                    .Where(x => (x.Value as Paragraph).ParagraphProperties != null)
                    .Where(x => (x.Value as Paragraph).ParagraphProperties.ParagraphStyleId != null)
                    .Where(x => idList.Contains((x.Value as Paragraph).ParagraphProperties?.ParagraphStyleId?.Val?.Value?.ToLower()))
                    .Where(x => x.Index >= lowerLimit)
                    .Where(x => x.Index <= upperLimit)
                    .ToList();

            if (result.Count == 0)
                return new KeyValuePair<int, string>(-1, "");

            KeyValuePair<int, string> returnValue = new KeyValuePair<int, string>(result.First().Index, result.First().Value.InnerText);

            return returnValue;
        }
        public Dictionary<int, string> FindManyBetween(OpenXmlElementList source, string id, int lowerLimit, int upperLimit, int dashIndex = -1)
        {
            if (dashIndex == -1)
                dashIndex = source.Count;

            var result = source
                     .Select((x, i) => new { Value = x, index = i })
                     .Where(x => x.index < dashIndex)
                     .Where(x => (x.Value is Paragraph))
                     .Where(x => (x.Value as Paragraph).ParagraphProperties != null)
                     .Where(x => (x.Value as Paragraph).ParagraphProperties.ParagraphStyleId != null)
                     .Where(x => (x.Value as Paragraph).ParagraphProperties?.ParagraphStyleId?.Val?.Value?.ToLower() == id.ToLower())
                     .Where(x => x.index >= lowerLimit)
                     .Where(x => x.index <= upperLimit)
                     .ToList();

            Dictionary<int, string> returnDictionary = new Dictionary<int, string>();

            foreach (var item in result)
                returnDictionary.Add(item.index, item.Value.InnerText);

            return returnDictionary;
        }
        public Dictionary<int, string> FindManyBetween(OpenXmlElementList source, List<string> idList, int lowerLimit, int upperLimit, int dashIndex = -1)
        {
            if (dashIndex == -1)
                dashIndex = source.Count;

            idList = idList.ConvertAll(d => d.ToLower());

            var result = source
                     .Select((x, i) => new { Value = x, index = i })
                     .Where(x => x.index < dashIndex)
                     .Where(x => (x.Value is Paragraph))
                     .Where(x => (x.Value as Paragraph).ParagraphProperties != null)
                     .Where(x => (x.Value as Paragraph).ParagraphProperties.ParagraphStyleId != null)
                     .Where(x => idList.Contains((x.Value as Paragraph).ParagraphProperties.ParagraphStyleId.Val.Value.ToLower()))
                     .Where(x => x.index >= lowerLimit)
                     .Where(x => x.index <= upperLimit)
                     .ToList();

            Dictionary<int, string> returnDictionary = new Dictionary<int, string>();

            foreach (var item in result)
                returnDictionary.Add(item.index, item.Value.InnerText);

            return returnDictionary;
        }
        public int FindElementEndIndex(OpenXmlElementList source, int startIndex, List<string> styleBreakerList, int dashIndex)
        {
            int returnValue = -1;
            for (int i = startIndex; i <= dashIndex; i++)
            {
                styleBreakerList.ForEach(x =>
                {
                    if (this.Validator.IsCurrentSomething(source, i, x))
                    {
                        returnValue = i - 1;
                        return;
                    }
                });

                if (returnValue == -1 && this.Validator.IsCurrentSomething(source, i, CRTA))
                {
                    return i - 1;
                }
                if (returnValue != -1)
                {
                    return returnValue;
                }
            }

            if (returnValue == -1)
            {
                throw new InvalidDataException("PARSER_FIND_ELEMENT_END_INDEX: Napaka formata dokumenta!");
            }
            return returnValue;
        }
        public ImageConvertTransferData FindImage(WordprocessingDocument wordproccessingDocument, Paragraph par)
        {
            if (par == null)
                throw new FormatException("Napaka formata! Stil slika nije dobro postavljen (ili je postavljen na nešto što nije slika)");

            foreach (Run run in par.Descendants<Run>())
            {
                DocumentFormat.OpenXml.Wordprocessing.Drawing image = run.Descendants<DocumentFormat.OpenXml.Wordprocessing.Drawing>().FirstOrDefault();

                if (image != null)
                {
                    var imageFirst = run.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.Picture>().FirstOrDefault();
                    var blip = imageFirst.BlipFill.Blip.Embed.Value;
                    ImagePart img = (ImagePart)wordproccessingDocument.MainDocumentPart.GetPartById(blip);
                    return new ImageConvertTransferData { Run = null, ImageData = null, ImagePart = img };
                }

                /*
                throw new FormatException( "Greška formata! Slika je u krivom formatu (ili nešto drugo osim slike ima stil slika)." +
                    "Pokušajte sliku zamjeniti sa slikom te slike (uslikajte zaslon tj. print screen i izrežite taj dio i zamjenite).");
                    */

                // don't do this (image cannot be shown in html 

                var imageV2 = run.Descendants<DocumentFormat.OpenXml.Wordprocessing.Picture>().FirstOrDefault();

                //ImageData
                if (imageV2 != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(imageV2.OuterXml);
                    XmlNodeList elemList = doc.GetElementsByTagName("v:imagedata");
                    var id = elemList[0]?.Attributes["r:id"]?.Value;

                    if (id == null)
                        throw new FormatException("Greška sa slikom!");

                    ImagePart img = (ImagePart)wordproccessingDocument.MainDocumentPart.GetPartById(id);
                    ImageData imageData = run.Descendants<ImageData>().FirstOrDefault();

                    return new ImageConvertTransferData { Run = run, ImageData = imageData };
                }


            }
            throw new FormatException("Napaka formata! Stil slika krivo postavljen! Smije biti samo na slikama!");
        }
        public int FindStartIndexOfNextSection(OpenXmlElementList source, int startIndex, int dashIndex)
        {
            for (int i = startIndex; i < dashIndex; i++)
            {
                if (this.Validator.IsCurrentSomething(source, i, CLEN_NASLOV) || this.Validator.IsCurrentSomething(source, i, ODSTAVEK_NASLOV)
                    || this.Validator.IsCurrentSomething(source, i, ALINEJA_NASLOV) || this.Validator.IsCurrentSomething(source, i, CRTA))
                {
                    return i;
                }
            }
            return dashIndex;
        }



        public string GetParagraphStyle(OpenXmlElementList source, int currentPositionOfI)
        {
            if (!Validator.IsXmlElementValidParagraphWithValidProperties(source[currentPositionOfI]))
                return "";

            var tempParagraph = source[currentPositionOfI] as Paragraph;
            return tempParagraph.ParagraphProperties.ParagraphStyleId.Val.Value.ToLower();
        }
        public string GetStyleFromParagraph(OpenXmlElement el)
        {

            if (!Validator.IsXmlElementValidParagraphWithValidProperties(el))
                throw new Exception("GetStyleFromParagraph: Element is not paragraph with valid style!");

            return (el as Paragraph).ParagraphProperties.ParagraphStyleId.Val.Value;
        }
        public string GetImageInHtmlBase64Format(WordprocessingDocument wordprocessingDocument, Paragraph paragraph, ImagePart image)
        {
            if (image == null)
                return null;

            var stream = image.GetStream();
            long length = stream.Length;
            byte[] byteStream = new byte[length];
            stream.Read(byteStream, 0, (int)length);

            string base64 = System.Convert.ToBase64String(byteStream);
            string dataString = "<img src=data:" + image.ContentType.ToString() + ";" + "base64," + base64 + ">";


            return dataString;
        }
        public string GetIndentionTextFromParagraph(WordprocessingDocument doc, int elementPosition)
        {
            XDocument xDoc = doc.MainDocumentPart.GetXDocument();
            var xParagraph = xDoc.Descendants(W.p).ElementAt(elementPosition);

            string listItem = ListItemRetriever.RetrieveListItem(
                   doc, xParagraph, null);

            return listItem;
        }
        public List<string> GetAllDocumentStyles(OpenXmlElementList source, int dashIndex)
        {
            List<string> styles = new List<string>();
            for(int i = 0; i < dashIndex; i++)
            {
                string style = GetParagraphStyle(source, i);

            }
            return styles;
        }



        public int CountStyle(OpenXmlElementList source, string style, int dashIndex)
        {
            return FindMany(source, style.ToLower(), dashIndex).Count;
        }
        public int CountStyleBetween(OpenXmlElementList source, string style, int dashIndex, int lowerLimit, int upperLimit)
        {
            return FindManyBetween(source, style.ToLower(), lowerLimit, upperLimit, dashIndex).Count;
        }

    }
}
