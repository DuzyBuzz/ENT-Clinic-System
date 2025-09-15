using System;
using System.Collections.Generic;
using System.Text;

namespace ENT_Clinic_System.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Shortens a string to a readable abbreviation.
        /// If too long, keeps only vowels (a, e, i, o, u) from each word.
        /// Example: "Chicken Alfredo" -> "Chkn Alfd" or "ie Ae" if shortened further
        /// </summary>
        public static string ShortenReadable(string text, int maxLength = 12)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> resultWords = new List<string>();
            StringBuilder result = new StringBuilder();

            foreach (var word in words)
            {
                resultWords.Add(word);
            }

            string joined = string.Join(" ", resultWords);

            if (joined.Length <= maxLength)
                return joined;

            // If too long, keep first 2 letters + vowels
            result.Clear();
            foreach (var word in words)
            {
                StringBuilder sb = new StringBuilder();

                // Take first 2 letters
                if (word.Length >= 2)
                    sb.Append(word.Substring(0, 2));
                else
                    sb.Append(word);

                // Append vowels from the rest
                for (int i = 2; i < word.Length; i++)
                {
                    char c = word[i];
                    if ("aeiouAEIOU".IndexOf(c) >= 0)
                        sb.Append(c);
                }

                result.Append(sb.ToString() + " ");
            }

            // Trim and ensure maxLength
            string final = result.ToString().Trim();
            if (final.Length > maxLength)
                final = final.Substring(0, maxLength).Trim();

            return final;
        }
    }
}
