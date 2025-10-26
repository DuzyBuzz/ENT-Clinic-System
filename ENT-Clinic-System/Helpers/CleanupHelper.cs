using System;
using System.IO;

namespace ENT_Clinic_System.Helpers
{
    internal static class CleanupHelper
    {
        // Deletes the Image and Video folders next to the executable
        public static void DeleteImageAndVideoFolders()
        {
            string exeFolder = AppDomain.CurrentDomain.BaseDirectory; // path of your .exe

            string[] folders = { "Image", "Video", "LabFiles"};

            foreach (var folderName in folders)
            {
                try
                {
                    string fullPath = Path.Combine(exeFolder, folderName);
                    if (Directory.Exists(fullPath))
                    {
                        Directory.Delete(fullPath, true); // true = delete recursively
                        Console.WriteLine($"{folderName} folder deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete {folderName} folder: {ex.Message}");
                }
            }
        }


    }
}
