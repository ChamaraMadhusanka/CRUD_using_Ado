using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_using_Ado.Services;
using CRUD_using_Ado.Models;

namespace CRUD_using_Ado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository dataRepository;

        public DataController(IDataRepository repository)
        {
            dataRepository = repository;
        }

        [HttpGet("GetDataUsingDirectQuery")]
        public IActionResult GetDataUsingDirectQuery(string query)
        {
            var result = dataRepository.GetDataUsingDirectQuery(query);
            return StatusCode((int)result.statusCode, result);
        }

        [HttpGet("GetDataWithoutParameters")]
        public IActionResult GetDataWithoutParameters(string proName)
        {
            var result = dataRepository.GetDataWithoutParaValues(proName);
            return StatusCode((int)result.statusCode, result);
        }

        [HttpGet("GetDataWithParametersUsingHeader")]
        public IActionResult GetDataWithParametersUsingHeader(string proName, string paraNames, string paraValues)
        {
            var result = dataRepository.GetDataWithParaMetersInHeader(proName, paraNames, paraValues);
            return StatusCode((int)result.statusCode, result);
        }

        [HttpPost("GetDataWithParametersUsingHeader")]
        public IActionResult GetDataWithParametersUsingBody([FromBody] ProData proData)
        {
            var result = dataRepository.GetDataWithParaMetersInBody(proData);
            return StatusCode((int)result.statusCode, result);
        }

        [HttpPost("InsertDataUsingHeader")]
        public IActionResult InsertDataUsingHeader(string proName, string paraNames, string paraValues)
        {
            var result = dataRepository.InsertDatawithHeader(proName, paraNames, paraValues);
            return StatusCode((int)result.statusCode, result);
        }

        [HttpPost("InsertDataUsingBody")]
        public IActionResult InsertDataUsingBody([FromBody] ProData proData)
        {
            var result = dataRepository.InsertDatawithBody(proData);
            return StatusCode((int)result.statusCode, result);
        }
    }
}
