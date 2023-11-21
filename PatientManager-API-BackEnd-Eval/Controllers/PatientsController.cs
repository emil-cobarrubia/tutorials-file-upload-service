using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManager_API_BackEnd_Eval.Models;
using Microsoft.AspNetCore.JsonPatch;
using PatientManager_API_BackEnd_Eval.Services;
using PatientManager_API_BackEnd_Eval.Repositories;

namespace PatientManager_API_BackEnd_Eval.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private IPatientService patientService;

        public PatientsController()
        {
            this.patientService = new PatientService(new PatientRepository());
        }

        [HttpGet]
        public async Task<ActionResult> GetPatients([FromQuery] int? id, [FromQuery] string? name, [FromQuery] string? orderByAttribute)
        {
            List<Patient> patients = this.patientService.GetPatients(id, name, orderByAttribute);

            return Ok(patients); //Success 200
        }

        [HttpPost]
        public async Task<ActionResult> AddPatient(Patient p)
        {
            bool success = this.patientService.AddPatient(p);

            if (!success)
                return Conflict(); //409 //BadRequest(); //Fail 400

            return Created("", p); //Success 201
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePatient([FromQuery] int id, Patient p)
        {
            bool success = this.patientService.UpdatePatient(id, p);

            if (!success)
                return NotFound(); //Fail 404

            return NoContent();//Success 204
        }

        [HttpPatch]
        public async Task<ActionResult> UpdatePatient([FromQuery] int id, JsonPatchDocument<Patient> patientUpdates)
        {
            bool success = this.patientService.UpdatePatient(id, patientUpdates);

            if (!success)
                return NotFound(); //Fail 404

            return NoContent();//Success 204
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePatient([FromQuery] int? id)
        {
            bool success = this.patientService.DeletePatient(id);
            if (!success)
                return BadRequest();

            return NoContent();
        }
    }
}
