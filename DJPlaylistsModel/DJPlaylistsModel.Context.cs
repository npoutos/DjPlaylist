﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DJPlaylistsModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DJPlaylistEntities : DbContext
    {
        public DJPlaylistEntities()
            : base("name=DJPlaylistEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<GENRE> GENRES { get; set; }
        public virtual DbSet<PLAYLIST_SONG> PLAYLIST_SONGS { get; set; }
        public virtual DbSet<PLAYLIST> PLAYLISTS { get; set; }
        public virtual DbSet<SONG> SONGS { get; set; }
        public virtual DbSet<USER_PLAYLIST> USER_PLAYLISTS { get; set; }
        public virtual DbSet<SONG_PLAY> SONG_PLAYS { get; set; }
        public virtual DbSet<SONG_GENRE> SONG_GENRES { get; set; }
    }
}
