using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMT.Core.ServiceContracts;

namespace PMT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IOrganizationsService _organizationsService;

        public TestController(IOrganizationsService organizationsService)
        {
            _organizationsService = organizationsService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> TestDBConnection()
        {
            _ = await _organizationsService.GetAllOrganizationsAsync();

            return Ok();
        }
    }
}
