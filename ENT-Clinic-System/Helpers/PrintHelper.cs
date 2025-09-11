using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Handles printing of text with multi-page support.
    /// </summary>
    public class TextPrinter
    {
        private string textToPrint;   // The full text
        private int currentCharIndex; // Tracks current position in text

        public TextPrinter(string text)
        {
            textToPrint = text ?? string.Empty;
            currentCharIndex = 0;
        }

        /// <summary>
        /// Open preview and start printing.
        /// </summary>
        public void Print()
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50); // 0.5 inch margins
            pd.PrintPage += PrintPageHandler;

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = pd;
                preview.ShowDialog(); // Preview before print
            }
        }

        /// <summary>
        /// Print text across multiple pages automatically.
        /// </summary>
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 10);
            int charsFitted, linesFilled;

            // Available area inside margins
            RectangleF printArea = e.MarginBounds;

            // Measure how many characters fit on this page
            e.Graphics.MeasureString(
                textToPrint.Substring(currentCharIndex),
                font,
                printArea.Size,
                StringFormat.GenericDefault,
                out charsFitted,
                out linesFilled
            );

            // Print that portion
            e.Graphics.DrawString(
                textToPrint.Substring(currentCharIndex, charsFitted),
                font,
                Brushes.Black,
                printArea,
                StringFormat.GenericDefault
            );

            // Update position for next page
            currentCharIndex += charsFitted;

            // If more text remains, continue to next page
            if (currentCharIndex < textToPrint.Length)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                currentCharIndex = 0; // Reset for future prints
            }
        }
    }

    /// <summary>
    /// Handles printing of images with scaling to fit page.
    /// </summary>
    public class ImagePrinter
    {
        private Image imageToPrint;

        public ImagePrinter(Image image)
        {
            imageToPrint = image;
        }

        /// <summary>
        /// Open preview and start printing.
        /// </summary>
        public void Print()
        {
            if (imageToPrint == null)
            {
                MessageBox.Show("No image to print.");
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Margins = new Margins(20, 20, 20, 20); // smaller margins for image
            pd.PrintPage += PrintPageHandler;

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = pd;
                preview.ShowDialog();
            }
        }

        /// <summary>
        /// Print and scale the image to fit page while keeping aspect ratio.
        /// </summary>
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            Rectangle printArea = e.MarginBounds;

            // Maintain aspect ratio when scaling
            if ((double)imageToPrint.Width / imageToPrint.Height >
                (double)printArea.Width / printArea.Height)
            {
                // Fit by width
                int height = (int)(imageToPrint.Height * printArea.Width / (double)imageToPrint.Width);
                e.Graphics.DrawImage(imageToPrint, printArea.Left, printArea.Top, printArea.Width, height);
            }
            else
            {
                // Fit by height
                int width = (int)(imageToPrint.Width * printArea.Height / (double)imageToPrint.Height);
                e.Graphics.DrawImage(imageToPrint, printArea.Left, printArea.Top, width, printArea.Height);
            }
        }
    }
}
