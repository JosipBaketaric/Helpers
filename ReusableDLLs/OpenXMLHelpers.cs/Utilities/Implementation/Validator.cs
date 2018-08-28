using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using OpenXMLHelpers.Utilities.Interface;

namespace OpenXMLHelpers.Utilities.Implementation
{
    public class Validator : IValidator
    {
        //public IGetter Getter { get; set; }
        public List<string> ALLOWED_STYLES { get; set; }

        public void ValidateResultCount(OpenXmlElementList source, string style, bool isZeroCountValid, bool isMultipleCountValid, int dashIndex, int styleCount)
        {
            //int styleCount = Finder.FindMany(source, style.ToLower(), dashIndex).Count;
            if (styleCount == 0 && !isZeroCountValid)
            {
                throw new InvalidDataException("PARSER_VALIDATE_RESULT_COUNT: Greška formata dokumenta! Ne postoji stil: " + style);
            }

            if (styleCount > 1 && !isMultipleCountValid)
            {
                throw new InvalidDataException("PARSER_VALIDATE_RESULT_COUNT: Greška formata dokumenta! Postoji više stilova: " + style);
            }



        }
        public void ValidateResultCountBetween(OpenXmlElementList source, string style, bool isZeroCountValid, bool isMultipleCountValid, int lowerLimit, int upperLimit, int dashIndex, int styleCount)
        {
            //int styleCount = Finder.FindManyBetween(source, style.ToLower(), lowerLimit, upperLimit, dashIndex).Count;
            if (styleCount == 0 && !isZeroCountValid)
            {
                throw new InvalidDataException("PARSER_VALIDATE_RESULT_COUNT_BETWEEN: Greška formata dokumenta! Ne postoji stil: " + style);
            }

            if (styleCount > 1 && !isMultipleCountValid)
            {
                throw new InvalidDataException("PARSER_VALIDATE_RESULT_COUNT_BETWEEN: Greška formata dokumenta! Postoji više stilova: " + style);
            }



        }
        public void ValidateResultNullOrEmpty(string result, string style)
        {
            if (string.IsNullOrEmpty(result))
            {
                throw new InvalidDataException("PARSER_VALIDATE_RESULT_NULL_OR_EMPTY: Greška formata dokumenta! Ne postoji vrijednost za stil: " + style);
            }
        }
        public void ValidateDocumentStyles(List<string> style)
        {
            ALLOWED_STYLES = ALLOWED_STYLES.ConvertAll(x => x.ToLower());
            for(int i = 0; i < style.Count; i++)
            {
                if (!ALLOWED_STYLES.Contains(style.ElementAt(i).ToLower()))
                {
                    throw new InvalidDataException("PARSER_VALIDATE_DOCUMENT_STYLES: Greška formata dokumenta! Nedozvoljen stil: " + style);
                }
            }


        }
        public bool IsCurrentSomethingOf(OpenXmlElementList source, int currentPositionOfI, List<string> something)
        {
            something = something.ConvertAll(x => x.ToLower());

            try
            {
                if (currentPositionOfI >= source.Count)
                {
                    throw new Exception("Index out of range");
                }
                if (currentPositionOfI == -1)
                {
                    throw new Exception("wtf is this");
                }

                if (!IsXmlElementValidParagraphWithValidProperties(source[currentPositionOfI]))
                    return false;

                var tempParagraph = source[currentPositionOfI] as Paragraph;

                var id = tempParagraph.ParagraphProperties.ParagraphStyleId.Val.Value.ToLower();

                if (something.Contains(id))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool IsXmlElementValidParagraphWithValidProperties(OpenXmlElement element)
        {
            if (!(element is Paragraph))
                return false;
            var paragraph = element as Paragraph;

            if (paragraph.ParagraphProperties == null)
                return false;


            if (paragraph.ParagraphProperties.ParagraphStyleId == null)
                return false;

            return true;
        }
        public bool IsCurrentSomething(OpenXmlElementList source, int currentPositionOfI, string something)
        {
            try
            {
                if (currentPositionOfI >= source.Count)
                {
                    throw new Exception("Index out of range");
                }
                if (currentPositionOfI == -1)
                {
                    throw new Exception("wtf is this");
                }

                if (!IsXmlElementValidParagraphWithValidProperties(source[currentPositionOfI]))
                    return false;

                var tempParagraph = source[currentPositionOfI] as Paragraph;

                if (tempParagraph.ParagraphProperties.ParagraphStyleId.Val.Value.ToLower() == something.ToLower())
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

    }
}
