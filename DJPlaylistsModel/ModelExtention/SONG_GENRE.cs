using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class SONG_GENRE
    {
        public SONG_GENRE() { }

        public SONG_GENRE(SONG song,GENRE genre)
        {
            SONG_ID = song.ID;
            GENRE_ID = genre.ID;
        }
    }
}
