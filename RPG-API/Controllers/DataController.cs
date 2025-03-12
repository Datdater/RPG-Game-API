using BLL.DTO;
using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RPG_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _loadDataService;
        public DataController(IDataService loadDataService)
        {
            _loadDataService = loadDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData(string username)
        {
            var response = await _loadDataService.LoadData(username);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveData(DataSaveDTO data)
        {
            await _loadDataService.SaveData(data, "dathlecnx");
            return Ok();
        }
    }
}
