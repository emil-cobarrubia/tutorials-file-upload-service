using PatientManager_API_BackEnd_Eval.Models;

namespace PatientManager_API_BackEnd_Eval.Repositories
{
    public class FileUploadRepository : IFileUploadRepository
    {
        public FileUploadRepository() { }

        public FileUploadResponse SaveFile(IFormFile file, string fileName)
        {
            string folderName = Path.Combine("Resources", "UploadedFiles");
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                string guid = Guid.NewGuid().ToString();
                string uniqueFileName = Path.GetFileNameWithoutExtension(fileName) + "-" + guid + Path.GetExtension(fileName);

                string _fullPath = Path.Combine(pathToSave, uniqueFileName);
                string _dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(_fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return new FileUploadResponse() { Success = true, FullPath = _fullPath, DbPath = _dbPath };
            }
            else
            {
                return new FileUploadResponse { Success = false };
            }
        }
    }

    public interface IFileUploadRepository
    {
        FileUploadResponse SaveFile(IFormFile file, string fileName);
    }
}
