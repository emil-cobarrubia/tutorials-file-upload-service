using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManager_API_BackEnd_Eval.Services;
using System.Net.Http.Headers;
using PatientManager_API_BackEnd_Eval.Repositories;

namespace PatientManager_API_BackEnd_Eval.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private IFileUploadService fileService;

        public FileUploadController()
        {
            this.fileService = new FileUploadService(new FileUploadRepository(), new PatientRepository());
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                IFormCollection formCollection = await Request.ReadFormAsync();
                IFormFile file = formCollection.Files.First();
                if (file == null || file.Length == 0)
                    return BadRequest();

                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                //Move to Service and Repository Layer
                bool succes = this.fileService.UploadFile(file, fileName);

                if (!succes)
                    return BadRequest();
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
