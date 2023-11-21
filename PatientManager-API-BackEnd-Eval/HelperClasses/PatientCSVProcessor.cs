using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using PatientManager_API_BackEnd_Eval.Models;
using PatientManager_API_BackEnd_Eval.Repositories;
namespace PatientManager_API_BackEnd_Eval.HelperClasses
{
    public class PatientCSVProcessor
    {
        public string fullPath;
        public List<Patient> patients;
        public IPatientRepository patientRepository;
        public PatientCSVProcessor(string _fullPath, IPatientRepository _patientRepository)
        {
            fullPath = _fullPath;
            patients = new List<Patient>();
            patientRepository = _patientRepository;

            if (fullPath != null)
                ParseCSV();
        }

        public void ParseCSV()
        {
            if (fullPath == null)
                return;

            using (var reader = new StreamReader(fullPath))
            {
                bool isFirstLine = true;
                while (!reader.EndOfStream)
                {
                    //Skip Header row
                    if (isFirstLine)
                    {
                        reader.ReadLine();
                        isFirstLine = false;
                        continue;
                    }

                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    DateTime dateNow = DateTime.Now;

                    patients.Add(
                                        new Patient()
                                        {
                                            FirstName = values[0],
                                            LastName = values[1],
                                            BirthDate = DateTime.Parse(values[2]),
                                            Gender = values[3],
                                            SystemStatus = "Active",
                                            DateCreated = dateNow,
                                            DateLastUpdate = dateNow
                                        }
                                    ); ;
                }
            }
        }
    }
}
