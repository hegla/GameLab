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
    public class GenGamesController : ControllerBase
    {
        private readonly gameindustryContext _context;

        public GenGamesController(gameindustryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonDataGenres")]
        public JsonResult JsonData()
        {
            var genres = _context.Genres.Include(c => c.Games).ToList();
            List<object> genGames = new List<object>();
            genGames.Add(new[] {"Жанр", "Кількість ігор" });
            foreach(var g in genres)
            {
                genGames.Add(new object[] { g.Name, g.Games.Count() });
            }
            return new JsonResult(genGames);
        }

    }
}