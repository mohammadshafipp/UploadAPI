using Microsoft.AspNetCore.Mvc;
using UploadAPI;
using UploadAPI.Models;

namespace UploadAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile? file)
        {
            try
            {
                // Checking the file is uploaded or not
                if (file == null || file.Length == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new FileUploadResponse { fileFullPath = "", Message = "File is empty or not selected." });
                }

                // Getting extension to check allowed or not
                string extension = FileUploadHelper.GetFileExtension(file.FileName);

                // Checking extension from the list of globally fixed allowed extensions
                if (!FileUploadHelper.allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    // Getting error message with supported list of extension
                    string ErrorMessage = FileUploadHelper.GetUnsupportedExtensionErrorMessage();
                    return StatusCode(StatusCodes.Status415UnsupportedMediaType, new FileUploadResponse { fileFullPath = "", Message = ErrorMessage });
                }

                // Checking the uploaded file is larger than the fixed size limit
                if (file.Length > FileUploadHelper.maxFileSize)
                {
                    // Getting error message with convertion of allowed MB
                    string ErrorMessage = FileUploadHelper.GetLargeFileErrorMessage();
                    return StatusCode(StatusCodes.Status413PayloadTooLarge, new FileUploadResponse { fileFullPath = "", Message = ErrorMessage });
                }

                // Creating unique file name to protect from replacement
                string timestamp = DateTime.Now.ToString("HH_mm_ss_ffff");
                string newFileFullName = $"{timestamp}_{file.FileName}";
                string newFileFullPath = Path.Combine(FileUploadHelper.fileUploadPath, newFileFullName);
                
                // Saving uploaded file into fixed local path
                try
                {
                    if (!Directory.Exists(FileUploadHelper.fileUploadPath))
                    {
                        Directory.CreateDirectory(FileUploadHelper.fileUploadPath);
                    }

                    using (var stream = new FileStream(newFileFullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return StatusCode(StatusCodes.Status200OK, new FileUploadResponse { fileFullPath = newFileFullPath, Message = "File uploaded successfully!" });
                }
                catch (IOException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new FileUploadResponse { fileFullPath = "", Message = "Internal server error while saving the file!" });
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FileUploadResponse { fileFullPath = "", Message = "Internal server error while validating the file!" });
            }
        }
    }
}