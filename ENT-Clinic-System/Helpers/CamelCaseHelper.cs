using System;
using System.Globalization;

namespace ENT_Clinic_System.Helpers
{
    internal static class CamelCaseHelper
    {
        /// <summary>
        /// Converts a string into Camel Case (Title Case).
        /// Example: "ilOiLo ciTY" -> "Iloilo City"
        /// </summary>
        public static string ToCamelCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // Use TextInfo for culture-aware conversion
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            // Convert the whole string to lowercase, then apply TitleCase
            string result = textInfo.ToTitleCase(input.ToLower());

            return result;
        }
    }
}
