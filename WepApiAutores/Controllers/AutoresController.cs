﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepApiAutores.Entidades;

namespace WepApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] // api/autores
        [HttpGet("listado")] // api/autores/listado
        [HttpGet("/listado")] // listado
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await _context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpGet("primero")] // api/autores/primero
        public async Task<ActionResult<Autor>> PrimerAutor()
        {
            return await _context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}/{param2=persona}/{param3?}")] //api/autores/1
        public async Task<ActionResult<Autor>> Get(int id, string param2, string param3)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpGet("{nombre}")] //api/autores/1
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            _context.Add(autor);
            await _context.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpPut("{id:int}")] // api/autores/1
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            var existe = await _context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Update(autor);
            await _context.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpDelete("{id:int}")] // api/autores/1
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Autores.AnyAsync(x => x.Id == id);
            
            if (!existe)
            {
                return NotFound();
            }

            //Marcando al autor que será removido
            _context.Remove(new Autor { Id = id });
            await _context.SaveChangesAsync();
            
            return Ok(id);
        }


    }
}
