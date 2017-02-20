using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeeJayPlayListCreationService.Models
{
    public class DeejayDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateRegistered { get; set; }

        public List<PlaylistDto> Playlists { get; set; }
        public List<SongPlayDto> SongPlays { get; set; }

        public DeejayDto(DJPlaylistsModel.USER deejay)
        {
            Id = deejay.ID;
            FirstName = deejay.FIRST_NAME;
            LastName = deejay.LAST_NAME;
            Email = deejay.EMAIL;

            Playlists = new List<PlaylistDto>();
            foreach(var playlist in deejay.PLAYLISTS)
            {
                Playlists.Add(new PlaylistDto(playlist));
            }

            SongPlays = new List<SongPlayDto>();
            foreach (var songPLay in deejay.SONG_PLAYS)
            {
                SongPlays.Add(new SongPlayDto(songPLay));
            }

        }
    }
}