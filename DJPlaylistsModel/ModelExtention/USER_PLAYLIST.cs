using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class USER_PLAYLIST
    {
        public USER_PLAYLIST() { }
        public USER_PLAYLIST(USER user, PLAYLIST playlist)
        {
            USER_ID = user.ID;
            PLAYLIST_ID = playlist.ID;
            DATE_CREATED = DateTime.Now;
        }
    }
}
