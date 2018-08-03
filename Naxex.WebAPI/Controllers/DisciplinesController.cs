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
using Naxex.Data;

namespace Naxex.WebAPI.Controllers
{
    public class DisciplinesController : ApiController
    {
        private NaxexEntities db = new NaxexEntities();

        // GET: api/Disciplines
        public IQueryable<Discipline> GetDisciplines()
        {
            return db.Disciplines;
        }

        // GET: api/Disciplines/5
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult GetDiscipline(int id)
        {
            Discipline discipline = db.Disciplines.Find(id);
            if (discipline == null)
            {
                return NotFound();
            }

            return Ok(discipline);
        }

        // PUT: api/Disciplines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscipline(int id, Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discipline.Id)
            {
                return BadRequest();
            }

            db.Entry(discipline).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplineExists(id))
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

        // POST: api/Disciplines
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult PostDiscipline(Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Disciplines.Add(discipline);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = discipline.Id }, discipline);
        }

        // DELETE: api/Disciplines/5
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult DeleteDiscipline(int id)
        {
            Discipline discipline = db.Disciplines.Find(id);
            if (discipline == null)
            {
                return NotFound();
            }

            db.Disciplines.Remove(discipline);
            db.SaveChanges();

            return Ok(discipline);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DisciplineExists(int id)
        {
            return db.Disciplines.Count(e => e.Id == id) > 0;
        }
    }
}