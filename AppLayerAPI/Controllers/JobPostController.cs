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

        [HttpGet("all")] //  https://localhost:7044/api/candidate/all
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")] // https://localhost:7044/api/candidate/1
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost("create")] //  https://localhost:7044/api/candidate/create
        public IActionResult Add(JobPostDTO JobPost)
        {
            return Ok(_service.Add(JobPost));
        }

        [HttpPut("update")] //  https://localhost:7044/api/candidate/update
        public IActionResult Update(JobPostDTO JobPost)
        {
            return Ok(_service.Update(JobPost));
        }

        [HttpDelete("delete/{id}")] //  https://localhost:7044/api/candidate/delete/1
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}
