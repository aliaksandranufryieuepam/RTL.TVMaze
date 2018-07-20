using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTL.TVMaze.Application.Contract;
using RTL.TVMaze.Application.Contract.Models;

namespace RTL.TVMaze.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Show>> Get([FromServices]IShowService service, int skip, int take)
        {
            // TODO: map to view model

            return await service.GetShowsAsync(take, skip);
        }
    }
}
