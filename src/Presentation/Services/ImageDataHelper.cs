namespace Presentation.Services
{
    /// <summary>
    /// Helper class to convert byte array (Uint8Array) to base64 string
    /// to pass to images in views (assuming JPEG format).
    /// </summary>
    public static class ImageDataHelper
    {
        public static string GetImageSource(Byte[] data)
        {
            string imageData = Convert.ToBase64String(data);
            return "data:image/jpeg;base64," + imageData;
        }
    }
}
