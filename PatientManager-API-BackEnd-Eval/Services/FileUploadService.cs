using System.Diagnostics;
using PatientManager_API_BackEnd_Eval.HelperClasses;
using PatientManager_API_BackEnd_Eval.Models;
using PatientManager_API_BackEnd_Eval.Repositories;
namespace PatientManager_API_BackEnd_Eval.Services
{
    public class FileUploadService : IFileUploadService
    {
        private IFileUploadRepository repository;
        private IPatientRepository patientRepository;
        public FileUploadService(IFileUploadRepository _repository, IPatientRepository _patientRepository)
        {
            repository = _repository;
            patientRepository = _patientRepository;
        }

        public bool UploadFile(IFormFile file, string fileName)
        {
            FileUploadResponse response = repository.SaveFile(file, fileName);
            if (!response.Success)
                return false;

            //Parse Data and hand to Patient Repository
            Debug.WriteLine(response.FullPath);
            PatientCSVProcessor csvProc = new PatientCSVProcessor(response.FullPath, patientRepository);


            //Nothing to parse
            if (csvProc.patients.Count() == 0)
                return false;

            //Validate and remove duplicates
            List<Patient> patientsNoDup = new List<Patient>();
            foreach(Patient p in csvProc.patients)
            {
                if(!this.patientRepository.Exists(p.FirstName, p.LastName, p.BirthDate))
                    patientsNoDup.Add(p);
            }
            
            this.patientRepository.AddMultiplePatients(patientsNoDup);

            return true;
        }
        public bool UploadPatientFile()
        {
            return true;
        }
    }

    public interface IFileUploadService
    {
        bool UploadFile(IFormFile file, string fileName);
        bool UploadPatientFile();
    }
}
