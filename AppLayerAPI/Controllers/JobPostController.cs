using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostController : ControllerBase
    {
        private readonly JobPostService _service;
        public JobPostController(JobPostService service)
        {
            _service = service;
        }

        [HttpGet("all")] //  https://localhost:7044/api/jobpost/all
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")] // https://localhost:7044/api/jobpost/1
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost("create")] //  https://localhost:7044/api/jobpost/create
        public IActionResult Add([FromBody] JobPostDTO JobPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_service.Add(JobPost));
        }

        [HttpPut("update")] //  https://localhost:7044/api/jobpost/update
        public IActionResult Update([FromBody] JobPostDTO JobPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_service.Update(JobPost));
        }

        [HttpDelete("delete/{id}")] //  https://localhost:7044/api/jobpost/delete/1
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}
