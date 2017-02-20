using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DJPlaylistsModel;
using DeeJayPlayListCreationService.Models;

namespace DeeJayPlayListCreationService.Controllers
{
    public class PLAYLISTSController : ApiController
    {
        private DJPlaylistEntities db = new DJPlaylistEntities();

        // GET: api/PLAYLISTS
        public IQueryable<PlaylistDto> GetPLAYLISTS()
        {
            var playlists = new List<PlaylistDto>();
            foreach (var playlist in Operations.GetAllPlaylists())
            {
                playlists.Add(new PlaylistDto(playlist));
            }
            return playlists.AsQueryable();

        }

        // GET: api/PLAYLISTS/5
        [ResponseType(typeof(PlaylistDto))]
        public async Task<IHttpActionResult> GetPLAYLIST(int id)
        {
            var PLAYLIST = Operations.GetPlaylist(id);  
            if (PLAYLIST == null)
            {
                return NotFound();
            }

            return Ok(new PlaylistDto(PLAYLIST));
        }

        // PUT: api/PLAYLISTS/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPLAYLIST(int id, PLAYLIST pLAYLIST)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pLAYLIST.ID)
            {
                return BadRequest();
            }

            db.Entry(pLAYLIST).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PLAYLISTExists(id))
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

        // POST: api/PLAYLISTS
        [ResponseType(typeof(PLAYLIST))]
        public async Task<IHttpActionResult> PostPLAYLIST(object o)
        {
            var action = (o as Newtonsoft.Json.Linq.JObject).SelectToken("Action").ToString();
            var userId = int.Parse((o as Newtonsoft.Json.Linq.JObject).SelectToken("UserId").ToString());
            var newName = (o as Newtonsoft.Json.Linq.JObject).SelectToken("NewPlaylistName").ToString();
            var playlistId = int.Parse((o as Newtonsoft.Json.Linq.JObject).SelectToken("PlaylistId").ToString());
            PLAYLIST playlist = null;
            if (action == "rename")
                playlist = Operations.RenamePlaylist(playlistId, newName);
            if (action == "create")
                playlist = Operations.CreatePlaylist(userId, newName);
            return CreatedAtRoute("DefaultApi", new { id = playlist.ID }, playlist);
        }

        // DELETE: api/PLAYLISTS/5
        [ResponseType(typeof(PLAYLIST))]
        public async Task<IHttpActionResult> DeletePLAYLIST(int id)
        {
            PLAYLIST pLAYLIST = await db.PLAYLISTS.FindAsync(id);
            if (pLAYLIST == null)
            {
                return NotFound();
            }

            db.PLAYLISTS.Remove(pLAYLIST);
            await db.SaveChangesAsync();

            return Ok(pLAYLIST);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PLAYLISTExists(int id)
        {
            return db.PLAYLISTS.Count(e => e.ID == id) > 0;
        }
    }
}