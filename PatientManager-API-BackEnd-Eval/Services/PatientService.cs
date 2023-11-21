//PatientService layer will be responsible for business logic and validations
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using PatientManager_API_BackEnd_Eval.Models;
using PatientManager_API_BackEnd_Eval.Repositories;

namespace PatientManager_API_BackEnd_Eval.Services
{
    public class PatientService : IPatientService
    {
        private IPatientRepository patientRepository;

        public PatientService(IPatientRepository repository)
        {
            patientRepository = repository;
        }
        public List<Patient> GetPatients(int? id, string? name, string? orderByAttribute)
        {
            List<Patient> patients = null;

            //Get All Patients
            if (id == null)
                patients = GetAllPatients(orderByAttribute);

            //Prioritize ID over name
            if (id != null)
                patients = GetPatientById(id);

            ////Get Patients by name
            if (name != null)
                patients = GetPatientsByName(name, orderByAttribute);

            return patients;
        }

        public List<Patient> GetAllPatients(string? orderByAttribute)
        {
            return patientRepository.GetAllPatients(orderByAttribute);
        }

        public List<Patient> GetPatientById(int? id)
        {
            List<Patient> patients = null;

            //Perform Validation
            if (id == null)
                return patients;

            patients = patientRepository.GetPatientById(id);

            return patients;
        }

        public List<Patient> GetPatientsByName(string? name, string? orderByAttribute)
        {
            List<Patient> patients = null;

            //Perform Validation
            if (name == null)
                return patients;

            patients = patientRepository.GetPatientsByName(name, orderByAttribute);

            return patients;
        }
        public bool AddPatient(Patient newPatient)
        {
            bool success = false;

            //Perform Validation
            if (newPatient.FirstName == null
                || newPatient.LastName == null
                || newPatient.BirthDate == null
            //    || newPatient.FirstName.Length >= 50
            //    || newPatient.LastName.Length >= 50
            //    || newPatient.Address1.Length >= 255
            //    || newPatient.Address2.Length >= 255
            //    || newPatient.City.Length >= 255
            //    || newPatient.State.Length >= 255
            //    || newPatient.Country.Length >= 255
            //    || newPatient.Zip.Length >= 10
                )
                return false;

            //Defaults
            newPatient.SystemStatus = newPatient.SystemStatus == null ? "Active" : newPatient.SystemStatus;
            newPatient.DateCreated = DateTime.Now;
            newPatient.DateLastUpdate = DateTime.Now;
            newPatient.Gender = newPatient.Gender == null ? "unknown" : newPatient.Gender;

            //Access DB
            return patientRepository.AddPatient(newPatient);
        }

        public bool UpdatePatient(int? id, JsonPatchDocument<Patient> patientUpdates)
        {
            //check if id exists
            List<Patient> patients = patientRepository.GetPatientById(id);
            if (patients.Count() == 0)
                return false;

            Patient patient = patients[0];

            patientUpdates.ApplyTo(patient);

            patient.DateLastUpdate = DateTime.Now;

            return patientRepository.UpdatePatient(id, patient);
        }

        public bool UpdatePatient(int? id, Patient p)
        {
            //check if id exists
            List<Patient> patients = patientRepository.GetPatientById(id);
            if (patients.Count() == 0)
                return false;

            return patientRepository.UpdatePatient(id, p);
        }

        public bool DeletePatient(int? id)
        {
            //Perform Validation
            if (id == null)
                patientRepository.DeleteAllPatients();

            return patientRepository.DeletePatient(id);
        }
    }

    public interface IPatientService
    {
        List<Patient> GetPatients(int? id, string? name, string? orderByAttribute);
        List<Patient> GetAllPatients(string? orderByAttribute);
        List<Patient> GetPatientById(int? id);
        List<Patient> GetPatientsByName(string? name, string? orderByAttribute);
        bool AddPatient(Patient newPatient);
        bool UpdatePatient(int? id, JsonPatchDocument<Patient> ppatientUpdates);
        bool UpdatePatient(int? id, Patient p);
        bool DeletePatient(int? id);
    }
}
