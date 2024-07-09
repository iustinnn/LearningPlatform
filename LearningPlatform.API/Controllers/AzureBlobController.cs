using LearningPlatform.App.Contracts;
using LearningPlatform.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.API.Controllers
{
    public class AzureBlobController : ApiControllerBase
    {
        public IAzureBlobService _service;
       public AzureBlobController(IAzureBlobService _azservice)
        {
            _service = _azservice;
        }



        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            var result = await _service.UploadFiles(files);
            return Ok(result);
        }

        [HttpGet("BlobList")]

        public async Task<IActionResult> GetBlobList()
        {
            var result = await _service.GetBlob();
            return Ok(result);
        }



       }
    }
