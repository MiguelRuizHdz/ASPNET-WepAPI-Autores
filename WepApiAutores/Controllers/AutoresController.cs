using Microsoft.AspNetCore.Mvc;
using WepApiAutores.Entidades;

namespace WepApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        [HttpGet] 
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>() { 
                new Autor() { Id = 1, Nombre = "Miguel" },
                new Autor() { Id = 2, Nombre = "Jorge" } 
            };
        }
    }
}
