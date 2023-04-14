using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Models.Validators
{
    public class FileValidator
    {
        public static bool ValidateFile(IFormFile file, int maxFileSizeMB, string[] allowedFileExtensions)
        {
            long maxFileSizeBytes = maxFileSizeMB * 1024 * 1024;
            if (file.Length > maxFileSizeBytes)
            {
                throw new FileSizeException($"The uploaded file is too large, the maximum logo size is {maxFileSizeMB} MB");
            }

            string fileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (string.IsNullOrEmpty(fileExtension) || !allowedFileExtensions.Contains(fileExtension))
            {
                string allowedFileExtensionsString = string.Join(", ", allowedFileExtensions);
                throw new BadFileExtension($"The sent logo can only be of the {allowedFileExtensionsString} type");
            }

            return true;
        }
    }
}
