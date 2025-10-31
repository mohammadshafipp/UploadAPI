namespace UploadAPI
{
    public static class FileUploadHelper
    {
        // Globally fixed file upload path
        public const string fileUploadPath = "D:\\Uploads";

        // Globally fixed file size
        public const long maxFileSize = 2 * 1024 * 1024;

        //Globally fixed file extensions
        public static string[] allowedExtensions = new string[] { ".pdf", ".jpg" };

        // Get file extension from file name
        public static string GetFileExtension(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            return extension;
        }

        // Get error message with allowed extension list
        public static string GetUnsupportedExtensionErrorMessage()
        {
            string Message = $"Only {string.Join(", ", allowedExtensions)} files are allowed!";
            return Message;
        }

        // Get error message with maximum file size
        public static string GetLargeFileErrorMessage()
        {
            string Message = $"Only {maxFileSize / (1024 * 1024)} MB files are allowed!";
            return Message;
        }
    }
}