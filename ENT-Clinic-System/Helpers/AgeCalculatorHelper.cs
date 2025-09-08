using System;

namespace ENT_Clinic_System.Helpers
{
    internal static class AgeCalculatorHelper
    {
        /// <summary>
        /// Calculates age based on birthdate.
        /// </summary>
        /// <param name="birthDate">The patient's birthdate</param>
        /// <returns>Age in years</returns>
        public static int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            // If birthday hasn't occurred yet this year, subtract 1
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age < 0 ? 0 : age; // safety: no negative ages
        }
    }
}
