using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeeJayPlayListCreationService.Models
{
    public class PlaylistDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DeejayId { get; set; }

        public string DeejayName { get; set; }

        public DateTime CreationDate { get; set; }

        public int SongCount { get; set; }

        public string PlayTime { get; set; }

        public List<PlaylistSongDto> PlaylistSongs { get; set; }

        public PlaylistDto(DJPlaylistsModel.PLAYLIST playlist)
        {
            Id = playlist.ID;
            Name = playlist.NAME;
            DeejayId = playlist.USER_ID;
            DeejayName = playlist.USER.FIRST_NAME +" "+ playlist.USER.LAST_NAME;
            PlaylistSongs = new List<PlaylistSongDto>();
            foreach (var playlistSong in playlist.PLAYLIST_SONGS.OrderBy(i=>i.ORDER))
            {
                PlaylistSongs.Add(new PlaylistSongDto(playlistSong));
            }

            SongCount = playlist.SongCount;

            PlayTime = "-";
            if (playlist.PLAYLIST_SONGS != null)
            {
                int playtime = playlist.PLAYLIST_SONGS.Sum(i => i.SONG.DURATION_SECS);
                PlayTime = (playtime / 60).ToString() + " Mins and " + (playtime % 60).ToString() + " Secs";
            }




        }



    }
}