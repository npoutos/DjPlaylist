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
using DJPlaylistsModel;
using DeeJayPlayListCreationService.Models;
namespace DeeJayPlayListCreationService.Controllers
{
    public class SONGSController : ApiController
    {
        private DJPlaylistEntities db = new DJPlaylistEntities();

        // GET: api/SONGS
        public IQueryable<SongDto> GetSONGS()
        {
            //return db.SONGS;
            var songsDTO = new List<SongDto>();
            foreach (var song in Operations.GetAllSongs())
            {
                songsDTO.Add(new SongDto(song));
            }
            return songsDTO.AsQueryable();
        }



        // GET: api/SONGS/5
        [ResponseType(typeof(SONG))]
        public IHttpActionResult GetSONG(int id)
        {
            SONG sONG = db.SONGS.Find(id);
            if (sONG == null)
            {
                return NotFound();
            }

            return Ok(sONG);
        }

        // PUT: api/SONGS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSONG(int id, SONG sONG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sONG.ID)
            {
                return BadRequest();
            }

            db.Entry(sONG).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SONGExists(id))
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

        // POST: api/SONGS
        [ResponseType(typeof(SONG))]
        public IHttpActionResult PostSONG(SONG sONG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SONGS.Add(sONG);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sONG.ID }, sONG);
        }

        // DELETE: api/SONGS/5
        [ResponseType(typeof(SONG))]
        public IHttpActionResult DeleteSONG(int id)
        {
            SONG sONG = db.SONGS.Find(id);
            if (sONG == null)
            {
                return NotFound();
            }

            db.SONGS.Remove(sONG);
            db.SaveChanges();

            return Ok(sONG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SONGExists(int id)
        {
            return db.SONGS.Count(e => e.ID == id) > 0;
        }
    }
}