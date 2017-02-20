using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class PLAYLIST_SONG
    {
        public PLAYLIST_SONG() { }

        public PLAYLIST_SONG(PLAYLIST playlist, SONG song,int order,bool allowDuplicates=true)
        {
            if (!allowDuplicates && playlist.PLAYLIST_SONGS.Any(i => i.SONG_ID == SONG_ID))
                throw new Exception("Playlist doesn't allows duplicate songs");
            if (playlist.PLAYLIST_SONGS.Any(i => i.ORDER == order))
                playlist.EmptyPosition(order);
            PLAYLIST_ID = playlist.ID;
            SONG_ID = song.ID;
            ORDER = order;
        }

        
    }
}
