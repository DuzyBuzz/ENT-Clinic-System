using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace ENT_Clinic_System.Helpers
{
    public static class PatientFileHelper
    {
        // Base folder for all patient files
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ENTClinic");

        /// <summary>
        /// Saves a bitmap image permanently and returns its full path.
        /// </summary>
        public static string SaveImage(int patientId, DateTime consultationDate, Bitmap image, string fileName)
        {
            try
            {
                string patientFolder = Path.Combine(BaseFolder, $"Patient_{patientId}", consultationDate.ToString("yyyyMMdd"), "Images");
                if (!Directory.Exists(patientFolder))
                    Directory.CreateDirectory(patientFolder);

                string fullPath = Path.Combine(patientFolder, fileName);
                image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

                return fullPath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error saving image: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Copies a video file to permanent folder and returns full path.
        /// </summary>
        public static string SaveVideo(int patientId, DateTime consultationDate, string sourcePath, string fileName)
        {
            try
            {
                string patientFolder = Path.Combine(BaseFolder, $"Patient_{patientId}", consultationDate.ToString("yyyyMMdd"), "Videos");
                if (!Directory.Exists(patientFolder))
                    Directory.CreateDirectory(patientFolder);

                string destinationPath = Path.Combine(patientFolder, fileName);
                File.Copy(sourcePath, destinationPath, true);

                return destinationPath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error saving video: " + ex.Message);
                return null;
            }
        }
    }
}
