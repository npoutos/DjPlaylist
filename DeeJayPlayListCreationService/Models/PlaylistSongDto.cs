using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeeJayPlayListCreationService.Models
{
    public class PlaylistSongDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public int Order { get; set; }
        public int SongPlays { get; set; }


        public SongDto Song {get;set;}

        public PlaylistSongDto(object playlistSongDto)
        {
            UserId = int.Parse((playlistSongDto as Newtonsoft.Json.Linq.JObject).SelectToken("DeejayId").ToString());
            PlaylistId = int.Parse((playlistSongDto as Newtonsoft.Json.Linq.JObject).SelectToken("PlaylistId").ToString());
            SongId = int.Parse((playlistSongDto as Newtonsoft.Json.Linq.JObject).SelectToken("SongId").ToString());
        }
        
        public PlaylistSongDto(DJPlaylistsModel.PLAYLIST_SONG plSong)
        {
            Id = plSong.ID;
            PlaylistId = plSong.PLAYLIST_ID;
            SongId = plSong.SONG_ID;
            UserId = plSong.PLAYLIST.USER_ID;
            SongPlays = plSong.SONG.SONG_PLAYS.Count;
            Song = new SongDto(plSong.SONG);
        }

    }
}