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
    public class PlaylistSongController : ApiController
    {
        private DJPlaylistEntities db = new DJPlaylistEntities();

        // GET: api/PlaylistSong
        public IQueryable<PlaylistSongDto> GetPlaylistSongDtoes()
        {
            var listPlaylistSongDtos = new List<PlaylistSongDto>();
            foreach (var plSong in Operations.GetAllPlaylistSongs())
                listPlaylistSongDtos.Add(new PlaylistSongDto(plSong)); 
            return listPlaylistSongDtos as IQueryable<PlaylistSongDto>;
        }

        // GET: api/PlaylistSong/5
        [ResponseType(typeof(PlaylistSongDto))]
        public async Task<IHttpActionResult> GetPlaylistSongDto(int id)
        {
            var plSong = Operations.GetPlaylistSong(id);
            if (plSong == null)
            {
                return NotFound();
            }

            return Ok(new PlaylistSongDto(plSong));
        }

        // PUT: api/PlaylistSong/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlaylistSongDto(int id, object playlistSongDto)
        {
            var action = (playlistSongDto as Newtonsoft.Json.Linq.JObject).SelectToken("Action").ToString();


            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PlaylistSong
        [ResponseType(typeof(PlaylistSongDto))]
        public async Task<IHttpActionResult> PostPlaylistSongDto(object playlistSongDto)
        {
            var action = (playlistSongDto as Newtonsoft.Json.Linq.JObject).SelectToken("Action").ToString();
            
            if (action == "add")
            {
                var newPlaylistSongDto = new PlaylistSongDto(playlistSongDto);
                newPlaylistSongDto = new PlaylistSongDto(Operations.AddSongToPlaylist(newPlaylistSongDto.UserId, newPlaylistSongDto.PlaylistId, newPlaylistSongDto.SongId));
                return CreatedAtRoute("DefaultApi", new { id = newPlaylistSongDto.Id }, newPlaylistSongDto);
            }

            if (action == "play")
            {
                var playlistSongId = int.Parse((playlistSongDto as Newtonsoft.Json.Linq.JObject).SelectToken("SongId").ToString());
                var playlistSong = new PlaylistSongDto(Operations.PlayPlaylistSong(playlistSongId));
                return CreatedAtRoute("DefaultApi", new { id = playlistSong.Id }, playlistSong);
            }

            return CreatedAtRoute("DefaultApi", null, new PlaylistSongDto(null));

        }

        // DELETE: api/PlaylistSong/5
        [ResponseType(typeof(PlaylistSongDto))]
        public async Task<IHttpActionResult> DeletePlaylistSongDto(int id)
        {
            var playlist =  Operations.RemoveSongFromPlaylist(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(new PlaylistDto(playlist));
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaylistSongDtoExists(int id)
        {
            throw new NotImplementedException();
            return false;
            //return db.PlaylistSongDtoes.Count(e => e.Id == id) > 0;
        }
    }
}