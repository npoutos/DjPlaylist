using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DJPlaylistsModel;
using System.ComponentModel.DataAnnotations;

namespace DeeJayPlayListCreationService.Models
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string TitleArtist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        
        public SongDto(SONG song)
        {
            Id = song.ID;
            Title = song.NAME;
            Artist = song.ARTIST_NAME;
            TitleArtist = Title + "/" + Artist;
            Album = song.ALBUM_NAME;
            foreach (var songGenre in song.SONG_GENRES)
            {
                Genre = Genre != null ? Genre + "/" + songGenre.GENRE.DESCRIPTION : songGenre.GENRE.DESCRIPTION;
            }
            Year = song.YEAR;
        }
    }
}