namespace PatientManager_API_BackEnd_Eval.Models
{
    public class FileUploadResponse
    {
        public bool Success { get; set; } = true;

        public string FullPath { get; set; }

        public string DbPath { get; set; }
    }
}
