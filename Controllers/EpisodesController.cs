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
    public class EpisodesController : ApiController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: api/Episodes
        public IQueryable<EpisodesClass> GetEpisodessobj()
        {
            return db.Episodessobj;
        }

        // GET: api/Episodes/5
        [ResponseType(typeof(EpisodesClass))]
        public IHttpActionResult GetEpisodesClass(int id)
        {
            EpisodesClass episodesClass = db.Episodessobj.Find(id);
            if (episodesClass == null)
            {
                return NotFound();
            }

            return Ok(episodesClass);
        }

        // PUT: api/Episodes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEpisodesClass(int id, EpisodesClass episodesClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != episodesClass.episode_id)
            {
                return BadRequest();
            }

            db.Entry(episodesClass).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpisodesClassExists(id))
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

        // POST: api/Episodes
        [ResponseType(typeof(EpisodesClass))]
        public IHttpActionResult PostEpisodesClass(EpisodesClass episodesClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Episodessobj.Add(episodesClass);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = episodesClass.episode_id }, episodesClass);
        }

        // DELETE: api/Episodes/5
        [ResponseType(typeof(EpisodesClass))]
        public IHttpActionResult DeleteEpisodesClass(int id)
        {
            EpisodesClass episodesClass = db.Episodessobj.Find(id);
            if (episodesClass == null)
            {
                return NotFound();
            }

            db.Episodessobj.Remove(episodesClass);
            db.SaveChanges();

            return Ok(episodesClass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EpisodesClassExists(int id)
        {
            return db.Episodessobj.Count(e => e.episode_id == id) > 0;
        }
    }
}