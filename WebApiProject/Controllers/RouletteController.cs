using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore;
using Entities;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {

        [HttpGet]
        [Route("createroulette")]
        public   IActionResult CreateRoulette()
        {
            RouletteService rs = new RouletteService();
            Response<Roulette> response = rs.CreateRoulette();
            if (response.RequestState)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet]
        [Route("getroulettes")]
        public IActionResult GetAll()
        {
            RouletteService rs = new RouletteService();
            Response<Roulette> response = rs.GetRoulettes();
            if (response.RequestState)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        [Route("openroulette")]
        public IActionResult OpenRoulette([FromBody] RouletteRequest request)
        {
            RouletteService rs = new RouletteService();
            Response<Roulette> response = rs.UpdateGameStateRoulette(request);
            if (response.RequestState)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpPut]
        [Route("betroulette")]
        public IActionResult BetRoulette([FromBody] RouletteRequest request)
        {
            RouletteService rs = new RouletteService();
            Response<Roulette> response = new Response<Roulette>();
            Response<Client> responseclient = new Response<Client>();
            Request.Headers.TryGetValue("IdClient", out var clientId);
            ClientService cs = new ClientService();
            responseclient=cs.ValidateCreditsClient(clientid: clientId.ToString(), money:request.Betmoney);
            if (responseclient != null)
            {
                return BadRequest(responseclient);
            }
            response = rs.UpdateBetRoulette(request:request,clientId:clientId);
            if (response.RequestState)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet]
        [Route("closeroulette")]
        public IActionResult CloseRoulette(string rouletteID)
        {
            RouletteService rs = new RouletteService();
            Response<Roulette> response = new Response<Roulette>();
            
           
            response = rs.CloseRoulette(RouletteId:rouletteID);
            if (response.RequestState)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
