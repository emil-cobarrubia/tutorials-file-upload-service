using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PatientManager_API_BackEnd_Eval.Contexts;
using PatientManager_API_BackEnd_Eval.Models;
using System;

namespace PatientManager_API_BackEnd_Eval.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private PrototypesDevContext dbContext = new PrototypesDevContext();

        public bool AddPatient(Patient newPatient)
        {
            bool success = true;

            if (newPatient == null)
                return false;

            IQueryable<Patient> patients = dbContext.Patients.Where(
                p => p.FirstName == newPatient.FirstName
                && p.LastName == newPatient.LastName
                && p.BirthDate == newPatient.BirthDate);

            if (patients.Count() > 0)
                return false;

            newPatient.DateCreated = DateTime.Now;
            newPatient.DateLastUpdate = DateTime.Now;
            newPatient.SystemStatus = "Active";

            this.dbContext.Patients.Add(newPatient);
            this.dbContext.SaveChanges();

            return success;
        }

        public bool AddMultiplePatients(List<Patient> patients)
        {
            bool success = true;

            if (patients == null)
                return false;

            this.dbContext.Patients.AddRange(patients);
            this.dbContext.SaveChanges();

            return success;
        }

        public List<Patient> GetAllPatients(string? orderByAttribute)
        {
            List<Patient> patients = null;

            if (orderByAttribute == null)
                patients = this.dbContext.Patients.ToList();
            else
            {
                if (orderByAttribute.ToUpper() == "FIRSTNAME")
                    patients = dbContext.Patients.OrderBy(p => p.FirstName).ToList();
                else if (orderByAttribute.ToUpper() == "LASTNAME")
                    patients = dbContext.Patients.OrderBy(p => p.LastName).ToList();
                else if (orderByAttribute.ToUpper() == "BIRTHDATE")
                    patients = dbContext.Patients.OrderBy(p => p.BirthDate).ToList();
                else if (orderByAttribute.ToUpper() == "GENDER")
                    patients = dbContext.Patients.OrderBy(p => p.Gender).ToList();
            }

            return patients;
        }
        public List<Patient> GetPatientById(int? id)
        {
            List<Patient> patients = null;

            patients = this.dbContext.Patients.Where(p => p.Pid == id).ToList();

            return patients;
        }

        public List<Patient> GetPatientsByName(string? name, string? orderByAttribute)
        {
            List<Patient> patients = null;

            patients = this.dbContext.Patients.Where(p => p.FirstName.Contains(name)
                || p.LastName.Contains(name)).ToList();

            return patients;
        }

        public bool UpdatePatient(int? id, Patient pNewUpdates)
        {
            List<Patient> patients = null;
            patients = this.dbContext.Patients.Where(p => p.Pid == id).ToList();
            if (patients.Count() == 0)
                return false;

            var p = patients[0];
            p.FirstName = pNewUpdates.FirstName;
            p.LastName = pNewUpdates.LastName;
            p.Gender = pNewUpdates.Gender; 
            p.BirthDate = pNewUpdates.BirthDate;
            p.DateLastUpdate = DateTime.Now;

            this.dbContext.Patients.Update(p);
            this.dbContext.SaveChanges();

            return true;
        }

        public bool DeletePatient(int? id)
        {
            if (id == null)
                return false;

            this.dbContext.Patients.Where(p => p.Pid == id).ExecuteDelete();
            this.dbContext.SaveChanges();

            return true;
        }

        public bool DeleteAllPatients()
        {
            bool success = true;

            try 
            {
                this.dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE [Patients]");
            }
            catch(Exception e)
            {
                success = false; ;
            }
            
            return success;
        }

        public bool Exists(string firstName, string lastName, DateTime birthDate)
        {
            bool exists = this.dbContext.Patients.Where(p =>
                            p.FirstName == firstName
                            && p.LastName == lastName
                            && p.BirthDate == birthDate).Count() > 0;

            return exists;
        }
    }

    public interface IPatientRepository
    {
        List<Patient> GetAllPatients(string? orderByAttribute);
        bool AddPatient(Patient patient);
        List<Patient> GetPatientById(int? id);

        List<Patient> GetPatientsByName(string? name, string? orderByAttribute);

        bool AddMultiplePatients(List<Patient> patients);
        bool UpdatePatient(int? id, Patient p);
        bool DeletePatient(int? id);

        bool DeleteAllPatients();

        bool Exists(string firstName, string lastName, DateTime birthDate);
    }
}
