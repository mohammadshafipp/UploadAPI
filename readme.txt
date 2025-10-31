UploadAPI is a simple ASP.NET Core Web API for uploading files with built-in validation. It uses a response model and a helper class for centralized configuration.

Features
- Upload files via HTTP POST.
- Validate file type (.pdf, .jpg).
- Validate file size (max 2 MB).
- Returns structured JSON responses using FileUploadResponse.
- Centralized configuration in FileUploadHelper:
  - Allowed extensions
  - Maximum file size
  - File upload path
  - Error messages
- Handles errors:
  - Empty or missing file
  - Unsupported file type
  - File too large
  - Internal server errors
  
Configuration
All settings are in FileUploadHelper:
- fileUploadPath – Where files are saved
- allowedExtensions – Supported file types
- maxFileSize – Maximum file size
- Error messages – Customizable globally

API Endpoint
POST /api/Upload/UploadFile
Request: Form-data parameter 'file' (IFormFile)

Response Model: FileUploadResponse
- fileFullPath: string
- Message: string

Status Codes:
- 200 OK – File uploaded successfully
- 404 Not Found – File missing or empty
- 415 Unsupported Media Type – Invalid file extension
- 413 Payload Too Large – File exceeds max size
- 500 Internal Server Error – Validation or saving error