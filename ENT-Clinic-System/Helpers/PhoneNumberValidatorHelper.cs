using System;
using System.Text.RegularExpressions;

namespace ENT_Clinic_System.Helpers
{
    internal static class PhoneNumberValidatorHelper
    {
        /// <summary>
        /// Validates if a phone number is a valid PH mobile number.
        /// Format: 11 digits, starts with "09"
        /// Example: 09123456789
        /// </summary>
        public static bool IsValidPhilippineMobile(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Regex: must start with 09 and followed by 9 digits = 11 total
            return Regex.IsMatch(phoneNumber, @"^09\d{9}$");
        }
    }
}
