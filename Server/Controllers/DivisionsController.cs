using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : ControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(null);
        }
    }
}
