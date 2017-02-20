using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class PLAYLIST
    {
        public int SongCount { get { return PLAYLIST_SONGS != null ? PLAYLIST_SONGS.Count : 0; }                }

        public PLAYLIST(string name, int userId, bool isPublic)
        {
            NAME = name;
            USER_ID = userId;
            IS_PUBLIC = isPublic;
            DATE_CREATED = DateTime.Now;
        }

        
        public void EmptyPosition(int songOrder)
        {
            var plSong = PLAYLIST_SONGS.FirstOrDefault(i => i.ORDER == songOrder);
            if (plSong != null)
                ShiftSongDown(plSong);
        }

        private void ShiftSongDown(PLAYLIST_SONG plSong)
        {
            var songInTargetPosition = PLAYLIST_SONGS.FirstOrDefault(i => i.ORDER == plSong.ORDER + 1);
            if (songInTargetPosition != null)
                MoveSongDown(songInTargetPosition);
            plSong.ORDER = plSong.ORDER + 1;
        }

        public void MoveSongDown(PLAYLIST_SONG plSong)
        {
            if (!PLAYLIST_SONGS.Any(i => i.ORDER > plSong.ORDER)) return;

            var songInTargetPosition = PLAYLIST_SONGS.Where(i=>i.ORDER>plSong.ORDER).OrderBy(k=>k.ORDER).FirstOrDefault();
            var newPosition = songInTargetPosition.ORDER;
            songInTargetPosition.ORDER = plSong.ORDER;
            plSong.ORDER = newPosition;
        }

        public void MoveSongUp(PLAYLIST_SONG plSong)
        {
            if (!PLAYLIST_SONGS.Any(i => i.ORDER < plSong.ORDER))
                return;
            var targetPlSong =
                PLAYLIST_SONGS.Where(i => i.ORDER < plSong.ORDER).OrderByDescending(k => k.ORDER).FirstOrDefault();
            int newPosition = targetPlSong.ORDER;
            targetPlSong.ORDER = plSong.ORDER;
            plSong.ORDER = newPosition;
        }


    }
}
