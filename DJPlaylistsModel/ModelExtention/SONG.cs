using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class SONG
    {
        public SONG(string name, string artistName, string albumName, int year,int durationSec)
        {
            NAME = name;
            ARTIST_NAME = artistName;
            ALBUM_NAME = albumName;
            YEAR = year;
            DURATION_SECS = durationSec;
        }
    }
}
