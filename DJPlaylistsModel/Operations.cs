using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public class Operations
    {
        #region UserSongCreation

        public static USER RegisterUser(string firstName, string lastName, string email)
        {
            var context = DJPlaylistContext.GetContext();
            if (context.USERS.Any(i => i.EMAIL == email))
                throw new Exception("There already a registered DJ associated to this email");
            var user = new USER(firstName, lastName, email);
            context.USERS.Add(user);
            context.SaveChanges();
            return user;
        }

        public static SONG AddSongToDB(string name, string artistName, string albumName, string genreName, int year,int durationSec)
        {
            return AddSongToDB(name, artistName, albumName, new List<string>() { genreName }, year, durationSec);
        }

        public static SONG AddSongToDB(string name, string artistName, string albumName, List<string> genreNames, int year, int durationSec)
        {
            var context = DJPlaylistContext.GetContext();

            var song = context.SONGS.FirstOrDefault(i => i.NAME == name && i.ARTIST_NAME == artistName && i.ALBUM_NAME == albumName && i.YEAR == year);
            if (song == null)
            {
                song = new SONG(name, artistName, albumName, year, durationSec);
                context.SONGS.Add(song);
                context.SaveChanges();
            }

            foreach (var genreName in genreNames)
            {
                var genre = context.GENRES.FirstOrDefault(i => i.DESCRIPTION == genreName);
                if (genre == null)
                {
                    genre = new GENRE(genreName);
                    context.GENRES.Add(genre);
                    context.SaveChanges();
                }
                var songGenre = context.SONG_GENRES.FirstOrDefault(i => i.SONG_ID == song.ID && i.GENRE_ID == genre.ID);
                if (songGenre == null)
                {
                    songGenre = new SONG_GENRE(song, genre);
                    context.SONG_GENRES.Add(songGenre);
                    context.SaveChanges();
                }
            }

            return song;

        } 
        #endregion

        #region PlayListManagement

        public static PLAYLIST CreatePlaylist(int userID, string name, bool isPublic = false)
        {
            var context = DJPlaylistContext.GetContext();
            var user = context.USERS.FirstOrDefault(i => i.ID == userID);
            if (user ==null)
                throw new Exception("User not found");
            if (context.PLAYLISTS.Any(i => i.NAME == name && i.USER_ID == userID))
                throw new Exception("There is already a playlist with this name");
            var playlist = new PLAYLIST(name, userID, isPublic);
            context.PLAYLISTS.Add(playlist);
            context.SaveChanges();
            var userPlaylist = new USER_PLAYLIST(user, playlist);
            context.USER_PLAYLISTS.Add(userPlaylist);
            context.SaveChanges();
            return playlist;
        }

        public static PLAYLIST RenamePlaylist( int playListID, string newName)
        {
            var context = DJPlaylistContext.GetContext();
            var playlist = context.PLAYLISTS.FirstOrDefault(i => i.ID == playListID);
            if (playlist == null)
                throw new Exception("Playlist Not Found");
            var userID = playlist.USER_ID;
            if (context.PLAYLISTS.Any(i => i.NAME == newName && i.USER_ID == userID))
                throw new Exception("There is already a playlist with this name");
            playlist.NAME = newName;
            context.SaveChanges();
            return playlist;
        }

        public static PLAYLIST_SONG AddSongToPlaylist(int userID, int playlistID, int songID)
        {
            var context = DJPlaylistContext.GetContext();
            var playlist = context.PLAYLISTS.FirstOrDefault(i => i.ID == playlistID);
            if (playlist == null)
                throw new Exception("Playlist not found");
            var song = context.SONGS.FirstOrDefault(i => i.ID == songID);
            if (song == null)
                throw new Exception("Song not found");
            if (playlist.USER.ID != userID)
                throw new Exception("Only owner can add songs to playlist");
            var order = playlist.PLAYLIST_SONGS.Any() ? playlist.PLAYLIST_SONGS.Max(i => i.ORDER) + 1 : 1;
            var plSong = new PLAYLIST_SONG(playlist, song, order);
            context.PLAYLIST_SONGS.Add(plSong);
            context.SaveChanges();
            return plSong;
        }

        public static PLAYLIST RemoveSongFromPlaylist(int playlistSongID)
        {
            var context = DJPlaylistContext.GetContext();
            var plSong = context.PLAYLIST_SONGS.FirstOrDefault(i => i.ID == playlistSongID);
            var playlist = plSong.PLAYLIST;
            if (plSong == null)
                throw new Exception("Playlist Song not found");
            //var playList = plSong.PLAYLIST;
            //if (plSong.PLAYLIST.USER_ID != userID)
            //    throw new Exception("Only owner can remove songs to playlist");
            context.PLAYLIST_SONGS.Remove(plSong);
            context.SaveChanges();
            return playlist;
            
        }



        public static PLAYLIST MoveSongDown(int userID, int playlistSongID)
        {
            return MoveSong(userID, playlistSongID, false);
        }

        public static PLAYLIST MoveSongUp(int userID, int playlistSongID)
        {
            return MoveSong(userID, playlistSongID, true);
        }

        private static PLAYLIST MoveSong(int userID, int playlistSongID,bool moveUp)
        {
            var context = DJPlaylistContext.GetContext();
            var plSong = context.PLAYLIST_SONGS.FirstOrDefault(i => i.ID == playlistSongID);
            if (plSong == null)
                throw new Exception("Playlist Song not found");
            var playList = plSong.PLAYLIST;
            if (plSong.PLAYLIST.USER_ID != userID)
                throw new Exception("Only owner can modify playlist");
            if (moveUp)
                playList.MoveSongUp(plSong);
            else
                playList.MoveSongDown(plSong);
            context.SaveChanges();
            return playList;
        }



        #endregion

        #region SongPlay
        public static SONG_PLAY PlaySong(int songID, int userId)
        {
            var context = DJPlaylistContext.GetContext();
            var song = context.SONGS.FirstOrDefault(i => i.ID == songID);
            if (song == null)
                throw new Exception("Song not found");
            var user = context.USERS.FirstOrDefault(i => i.ID == userId);
            if (user == null)
                throw new Exception("User not found");
            var songPlay = new SONG_PLAY(song, user);
            context.SONG_PLAYS.Add(songPlay);
            context.SaveChanges();
            return songPlay;
        }
        public static PLAYLIST_SONG PlayPlaylistSong(int id)
        {
            var context = DJPlaylistContext.GetContext();
            var playlistSong = context.PLAYLIST_SONGS.FirstOrDefault(i => i.ID == id);
            var songPlay = PlaySong(playlistSong.SONG_ID, playlistSong.PLAYLIST.USER_ID);

            return DJPlaylistContext.GetContext().PLAYLIST_SONGS.FirstOrDefault(i => i.ID == playlistSong.ID);
        }
        #endregion

        #region DataRetrieval
        public static SONG GetSong(int songID)
        {
            return DJPlaylistContext.GetContext().SONGS.FirstOrDefault(i => i.ID == songID);
        }

        public static IQueryable<SONG> GetAllSongs()
        {
            return DJPlaylistContext.GetContext().SONGS;
        }

        public static PLAYLIST GetPlaylist(int playlistId)
        {
            return DJPlaylistContext.GetContext().PLAYLISTS.FirstOrDefault(i => i.ID == playlistId);
        }

        public static IQueryable<PLAYLIST> GetAllPlaylists()
        {
            return DJPlaylistContext.GetContext().PLAYLISTS;
        }

        public static PLAYLIST_SONG GetPlaylistSong(int id)
        {
            return DJPlaylistContext.GetContext().PLAYLIST_SONGS.FirstOrDefault(i => i.ID == id);
        }

        public static IQueryable<PLAYLIST_SONG> GetAllPlaylistSongs()
        {
            return DJPlaylistContext.GetContext().PLAYLIST_SONGS;
        }


        public static List<USER> GetAllUsers()
        {
            return DJPlaylistContext.GetContext().USERS.ToList();
        } 
        #endregion

    }
}
