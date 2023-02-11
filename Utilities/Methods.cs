using System;
using Utilities.ExtensionsMethods;

namespace Utilities
{
    public static class Methods
    {

        /// <summary>
        /// Interpret given text as true or false. Null or empty value is returned as false. Not expected text throws an exception.
        /// </summary>
        /// <param name="textToInterpret"></param>
        /// <returns>true or false depending on the interpretations</returns>
        public static bool InterpretTextAsTrueOrFalse(string textToInterpret)
        {
            textToInterpret = textToInterpret.ClearWhiteSpacesAndToLower();

            if (string.IsNullOrWhiteSpace(textToInterpret) ||
                textToInterpret == "0" ||
                textToInterpret == "false" ||
                textToInterpret == "fałsz" ||
                textToInterpret == "falsz" ||
                textToInterpret == "nie" ||
                textToInterpret == "n")
            {
                return false;
            }
            else if (textToInterpret == "1" ||
                textToInterpret == "true" ||
                textToInterpret == "prawda" ||
                textToInterpret == "tak" ||
                textToInterpret == "t")
            {
                return true;
            }
            else
            {
                throw new Exception("Nieobsługiwany tekst w funkcji InterpretTextAsTrueOrFalse()");
            }
        }

    }
}
