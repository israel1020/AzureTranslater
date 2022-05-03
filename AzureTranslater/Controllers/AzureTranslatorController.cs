using AzureTranslater.Models;
using AzureTranslater.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureTranslater.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AzureTranslatorController : ControllerBase
    {
        private AzureTranslatorService service;
        public AzureTranslatorController(AzureTranslatorService service)
        {
            this.service = service;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> TranslatorText([FromBody] List<AzureTranslatorRequestBody> body)
        {
            var result = await service.Execute(body);
            return Ok(result);
        }
    }
}
