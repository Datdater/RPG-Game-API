using BLL.DTO;
using BLL.Interface;
using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPG_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly IDataService _loadDataService;
        public DataController(IDataService loadDataService)
        {
            _loadDataService = loadDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var username = HttpContext.User.FindFirst("username")?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Username not found in token.");
            }
            var response = await _loadDataService.LoadData(username);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveData(DataSaveDTO data)
        {
            var username = HttpContext.User.FindFirst("username")?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Username not found in token.");
            }

            await _loadDataService.SaveData(data, username);
            return Ok();
        }
        [HttpGet("ruby")]
        public async Task<IActionResult> GetRuby()
        {
            var username = HttpContext.User.FindFirst("username")?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Username not found in token.");
            }
            try
            {
                var result = await _loadDataService.LoadRuby(username);
                return Ok(new { ruby = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("ruby")]
        public async Task<IActionResult> SaveRuby(int ruby)
        {
            //return Ok();

            var username = HttpContext.User.FindFirst("username")?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Username not found in token.");
            }
            try
            {
                await _loadDataService.SaveRuby(ruby, username);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
