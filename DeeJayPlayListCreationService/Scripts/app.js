var ViewModel = function () {

    var self = this;
    self.playlistSongs = ko.observableArray();
    self.currentPlaylist = ko.observable();
    self.currentPlaylistName = ko.observable();
    self.newPlaylistName = ko.observable();
    self.songs = ko.observableArray();
    self.playlists = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.newPlaylistSong =
        {
            Action: ko.observable(),
            Id: ko.observable(),
            PlaylistId: ko.observable(),
            DeejayId: ko.observable(),
            SongId: ko.observable(),
            Order: ko.observable(),
            Song: ko.observable()
        }
    self.newPlaylistName =
    {
        Action: ko.observable(),
        UserId: ko.observable(),
        PlaylistId: ko.observable(),
        NewPlaylistName: ko.observable()
    }


    var songsUri = '/api/songs/';
    var playlistsUri = '/api/playlists/';
    var playlistsSongsUri = '/api/PlaylistSong/';

    function getSongs() {
        ajaxHelper(songsUri, 'GET').done(function (data) {
            self.songs(data);
        });
    }



    self.renamePlaylist = function (formElement) {
        var newPlaylistName = {
            Action: "rename",
            UserId:4,
            PlaylistId: self.currentPlaylist().Id,
            NewPlaylistName: self.newPlaylistName
        };
        ajaxHelper(playlistsUri, 'POST', newPlaylistName).done(function (item) {
            //self.getPlaylistDetail(self.currentPlaylist());
            location.reload();
        });
    }

    getSongs();


    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllPlaylists() {
        ajaxHelper(playlistsUri, 'GET').done(function (data) {
            self.playlists(data);
        });
    }

    self.getPlaylistDetail = function (item) {
        self.currentPlaylist(item);
        self.currentPlaylistName(item.Name);
        ajaxHelper(playlistsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    self.addPlaylistSong = function (formElement) {
        var playlistSong = {
            Action:"add",
            DeejayId: self.currentPlaylist().DeejayId,
            PlaylistId: self.currentPlaylist().Id,
            SongId: self.newPlaylistSong.Song().Id

        };

        ajaxHelper(playlistsSongsUri, 'POST', playlistSong).done(function (item) {
            self.getPlaylistDetail(self.currentPlaylist());
        });
    }

    self.deletePlaylistSong = function (item) {
        ajaxHelper(playlistsSongsUri + item.Id, 'DELETE').done(function (data) {
            self.getPlaylistDetail(self.currentPlaylist());
        });
    }

    self.playPlaylistSong = function (formElement) {
        var playlistSong = {
            Action: "play",
            DeejayId: self.currentPlaylist().DeejayId,
            SongId: formElement.Id

        };
        ajaxHelper(playlistsSongsUri, 'POST', playlistSong.SongId, playlistSong).done(function (item) {

            self.getPlaylistDetail(self.currentPlaylist());
        });
    }
    self.deletePlaylist = function (item) {
        ajaxHelper(playlistsUri + item.Id, 'DELETE').done(function (data) {
            self.getPlaylistDetail(self.currentPlaylist());
        });
    }

    // Fetch the initial data.
    getAllPlaylists();
};

ko.applyBindings(new ViewModel());


////////////////////////////



