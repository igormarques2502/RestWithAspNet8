using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetUdemy.Business;
using RestWithAspNetUdemy.Data.VO;

namespace RestWithAspNetUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpGet]
        [Route("downloadFile/{fileName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [Produces("application/json")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);
            if (buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                HttpContext.Response.Headers.Add("content-lenght", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            return new ContentResult();
        }

        [HttpPost]
        [Route("uploadFile")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(FileDetailVO))]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost]
        [Route("uploadMultipleFiles")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(List<FileDetailVO>))]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFiles([FromForm] List<IFormFile> files)
        {
            List<FileDetailVO> details = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }


    }
}
