namespace UploadAPI.Models
{
    public class FileUploadResponse
    {
        // Only available when the status is success
        public string fileFullPath { get; set; }
        // Response message to identify the status details
        public string Message { get; set; }
    }
}