namespace ENT_Clinic_System.Helpers
{
    public static class FirstLetterUpperHelper
    {
        /// <summary>
        /// Capitalizes only the first letter of the input string.
        /// The rest of the text remains unchanged.
        /// Example: "100mg" → "100mg", "paracetamol 500mg" → "Paracetamol 500mg"
        /// </summary>
        public static string ToFirstUpper(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = input.Trim();

            // If the first character is a letter, capitalize only that.
            return char.ToUpper(input[0]) + (input.Length > 1 ? input.Substring(1) : "");
        }
    }
}
