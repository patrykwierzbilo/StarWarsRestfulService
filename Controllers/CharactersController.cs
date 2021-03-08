using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StartWarsRestfulService.DataContext;
using StartWarsRestfulService.Models;

namespace StartWarsRestfulService.Controllers
{
    public class CharactersController : ApiController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: api/Characters
        public IQueryable<CharactersDTO> GetCharactersobj()
        {
            var names = db.Charactersobj.ToList();
            //var episodes = db.Episodessobj.ToList();

            List<CharactersDTO> list = new List<CharactersDTO>();
            foreach (var x in names)
            {
                Console.Out.WriteLine(x.name);
                list.Add(new CharactersDTO(x.name, new List<string>(), new List<string>()));
            }
            return list.AsQueryable();
        }
        /*
        // GET: api/Characters/5
        [ResponseType(typeof(CharactersClass))]
        public IHttpActionResult GetCharactersClass(int id)
        {
            CharactersClass charactersClass = db.Charactersobj.Find(id);
            if (charactersClass == null)
            {
                return NotFound();
            }

            return Ok(charactersClass);
        }

        // PUT: api/Characters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCharactersClass(int id, CharactersClass charactersClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != charactersClass.character_id)
            {
                return BadRequest();
            }

            db.Entry(charactersClass).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharactersClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Characters
        [ResponseType(typeof(CharactersClass))]
        public IHttpActionResult PostCharactersClass(CharactersClass charactersClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Charactersobj.Add(charactersClass);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = charactersClass.character_id }, charactersClass);
        }

        // DELETE: api/Characters/5
        [ResponseType(typeof(CharactersClass))]
        public IHttpActionResult DeleteCharactersClass(int id)
        {
            CharactersClass charactersClass = db.Charactersobj.Find(id);
            if (charactersClass == null)
            {
                return NotFound();
            }

            db.Charactersobj.Remove(charactersClass);
            db.SaveChanges();

            return Ok(charactersClass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CharactersClassExists(int id)
        {
            return db.Charactersobj.Count(e => e.character_id == id) > 0;
        }*/
    }
}