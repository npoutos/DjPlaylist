using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class SONG_PLAY
    {
        public SONG_PLAY() { }
        public SONG_PLAY(SONG song, USER user)
        {
            SONG_ID = song.ID;
            USER_ID = user.ID;
            DATE_PLAYED = DateTime.Now;
        }
    }
}
