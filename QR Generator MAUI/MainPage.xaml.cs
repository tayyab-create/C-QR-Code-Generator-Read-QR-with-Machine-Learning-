using CommunityToolkit.Maui.Storage;
using IronQr;
using IronSoftware.Drawing;

namespace QR_Generator_MAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnGenerateClicked(object sender, EventArgs e)
        {
            // Create a QR Code object
            QrCode myQr = QrWriter.Write(inputText.Text);

            // Save QR Code as a Bitmap
            AnyBitmap qrImage = myQr.Save();

            // Define a temporary file path
            string tempFilePath = Path.Combine(FileSystem.CacheDirectory, "tempQrCode.png");

            // Save the QR Code as a file
            qrImage.SaveAs(tempFilePath);

            // Load the image from the file and set it as the source for the Image control
            qrCodeImage.Source = ImageSource.FromFile(tempFilePath);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string tempFilePath = Path.Combine(FileSystem.CacheDirectory, "tempQrCode.png");

            // Check if the QR code file exists
            if (File.Exists(tempFilePath))
            {
                using var stream = new MemoryStream(File.ReadAllBytes(tempFilePath));
                var fileSaverResult = await FileSaver.Default.SaveAsync("QrCode.png", stream, CancellationToken.None);
                if (fileSaverResult.IsSuccessful)
                {
                    await DisplayAlert("Success", $"File is saved: {fileSaverResult.FilePath}", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to save the file", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "QR Code not found. Please generate it first.", "OK");
            }
        }
    }

}
