//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class PLAYLIST_SONG
    {
        public int ID { get; set; }
        public int PLAYLIST_ID { get; set; }
        public int SONG_ID { get; set; }
        public int ORDER { get; set; }
    
        public virtual PLAYLIST PLAYLIST { get; set; }
        public virtual SONG SONG { get; set; }
    }
}