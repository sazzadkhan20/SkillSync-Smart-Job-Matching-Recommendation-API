using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")] // https://localhost:7044/api/jobpost/1
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost("create")] //  https://localhost:7044/api/jobpost/create
        public async Task<IActionResult> Add([FromBody] JobPostDTO JobPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _service.AddAsync(JobPost));
        }

        [HttpPut("update/{id}")] //  https://localhost:7044/api/jobpost/update
        public async Task<IActionResult> Update([FromBody] JobPostDTO JobPost, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await _service.UpdateAsync(JobPost, id));
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

        [HttpDelete("delete/{id}")] //  https://localhost:7044/api/jobpost/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _service.DeleteAsync(id));
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

        // Advance Search & Filter Job Posts
        [HttpGet("search")]
        public async Task<IActionResult> Search(
                        [FromQuery] string? title,
                        [FromQuery] string[] skills,
                        [FromQuery] int minExp = 0,
                        [FromQuery] string? location = null,
                        [FromQuery] DateTime? postedAfter = null,
                        [FromQuery] string? sort="asc")
        {
            try
            {
                return Ok(await _service.SearchAsync(title,skills,minExp,location,postedAfter,sort));
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
    }
}
