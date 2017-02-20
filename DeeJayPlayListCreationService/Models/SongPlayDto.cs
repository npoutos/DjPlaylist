using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeeJayPlayListCreationService.Models
{
    public class SongPlayDto
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int DeejayId { get; set; }
        public System.DateTime DatePlayed { get; set; }

        public SongDto Song { get; set; }
        
        public SongPlayDto(DJPlaylistsModel.SONG_PLAY songPlay)
        {
            Id = songPlay.ID;
            SongId = songPlay.SONG_ID;
            DeejayId = songPlay.USER_ID;
            DatePlayed = songPlay.DATE_PLAYED;
            Song = new SongDto(songPlay.SONG);
        }

    }
}