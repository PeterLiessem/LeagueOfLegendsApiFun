using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiotAPITest.Data;
using RiotAPITest.Model;

namespace RiotAPITest.Controllers
{
    [ApiController]
    [Route("/api")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApiFetcher data = new ApiFetcher();

        [HttpGet("/user/{username}")]
        public ActionResult<UserModel> Get(string username)
        {
            UserModel um = data.GetLeagueUser(username);
            return Ok(um);
        }

        [HttpGet("/matchByUser/{username}")]
        public ActionResult<MatchCollection> GetPoop(string username)
        {
            MatchCollection mc = data.GetGamesForUser(username);
            return Ok(mc);
        }
    }
}
