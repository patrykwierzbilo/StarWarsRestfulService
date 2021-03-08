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
using StartWarsRestfulService.Services;

namespace StartWarsRestfulService.Controllers
{
    public class CharactersDTOController : ApiController
    {
        private CharactersService charactersService;

        public CharactersDTOController()
        {
            charactersService = new CharactersService(new ApplicationDBContext());
        }

        // GET: api/Characters
        public IQueryable<CharactersDTO> GetCharactersobj()
        {
            return charactersService.GetAll().AsQueryable();
        }
        
        // GET: api/Characters/5
        [ResponseType(typeof(CharactersClass))]
        public IHttpActionResult GetCharactersClass(int id)
        {
            CharactersDTO character = charactersService.Get(id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }
        
                // PUT: api/Characters/5
                [ResponseType(typeof(void))]
                public IHttpActionResult PutCharactersClass(int id, CharactersDTO characterDTO)
                {
            charactersService.DeleteCharacter(id);
            charactersService.CreateCharacter(characterDTO);
            return Ok();
                    //if (!ModelState.IsValid)
                    //{
                    //    return BadRequest(ModelState);
                    //}

                    //if (id != charactersClass.character_id)
                    //{
                    //    return BadRequest();
                    //}

                    //db.Entry(charactersClass).State = EntityState.Modified;

                    //try
                    //{
                    //    db.SaveChanges();
                    //}
                    //catch (DbUpdateConcurrencyException)
                    //{
                    //    if (!CharactersClassExists(id))
                    //    {
                    //        return NotFound();
                    //    }
                    //    else
                    //    {
                    //        throw;
                    //    }
                    //}

                    //return StatusCode(HttpStatusCode.NoContent);
                }
        
       // POST: api/Characters
       [ResponseType(typeof(CharactersDTO))]
        public IHttpActionResult PostCharactersClass(CharactersDTO charactersClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            charactersService.CreateCharacter(charactersClass);
            //db.Charactersobj.Add(charactersClass);
            //db.SaveChanges();
            return Ok();
            //return CreatedAtRoute("DefaultApi", new { id = charactersClass.character_id }, charactersClass);
        }
       
                // DELETE: api/Characters/5
                [ResponseType(typeof(CharactersClass))]
                public IHttpActionResult DeleteCharactersClass(int id)
                {
            //CharactersClass charactersClass = db.Charactersobj.Find(id);
            //if (charactersClass == null)
            //{
            //    return NotFound();
            //}

            //db.Charactersobj.Remove(charactersClass);
            //db.SaveChanges();

            //return Ok(charactersClass);
                charactersService.DeleteCharacter(id);
                return Ok();
                }
 /*
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