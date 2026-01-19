using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly CandidateService _service;
        public CandidateController(CandidateService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost("create")]
        public IActionResult Add(CandidateDTO candidate)
        {
            try
            {
                var result = _service.Add(candidate);

                return Ok(new
                {
                    success = true,
                    candidate = result,
               });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            
        }

        [HttpPut("update")]
        public IActionResult Update(CandidateDTO candidate)
        {
            return Ok(_service.Update(candidate));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }

    }
}
