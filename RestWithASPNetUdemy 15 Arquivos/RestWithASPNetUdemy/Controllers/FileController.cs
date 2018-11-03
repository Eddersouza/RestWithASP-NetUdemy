using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using Swashbuckle.AspNetCore.Annotations;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : Controller
    {
        private IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        /// <summary>
        /// Get persons in system.
        /// </summary>       
        /// <returns>Persons items in system.</returns>
        [HttpGet("")]
        [SwaggerResponse((200), Type = typeof(byte[]), Description = "Get File successfully.")]
        [SwaggerResponse((500), Description = "System Error.")]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            byte[] buffer = _fileBusiness.getPdfFile();

            if (buffer != null)
            {
                HttpContext.Response.ContentType = "application/pdf";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }
    }
}