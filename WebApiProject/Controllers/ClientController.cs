using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using ApplicationCore;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpPost]
        [Route("createclient")]
        public IActionResult CreateClient([FromBody] Client newclient)
        {
            ClientService cs = new ClientService();
            Response<Client> response = cs.CreateClient(client:newclient);
            if (response.RequestState)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        [Route("addcredits")]
        public IActionResult AddCredits([FromBody] Client newclient)
        {
            ClientService cs = new ClientService();
            Response<Client> response = cs.AddCreditsClient(client: newclient);
            if (response.RequestState)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
