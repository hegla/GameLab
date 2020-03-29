using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Sem2Lab1SQLServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevGamesController : ControllerBase
    {
        private readonly gameindustryContext _context;

        public DevGamesController(gameindustryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonDataDevelopers")]
        public JsonResult JsonData()
        {
            var developers = _context.Developers.Include(c => c.Games).ToList();
            List<object> devGames = new List<object>();
            devGames.Add(new[] { "Розробник", "Кількість ігор" });
            foreach (var d in developers)
            {
                devGames.Add(new object[] { d.Name, d.Games.Count() });
            }
            return new JsonResult(devGames);
        }
    }

}