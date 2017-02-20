using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class DJPlaylistContext
    {
        private static string connectionString =
        //        "metadata=res://*/DJPlaylistsModel.csdl|res://*/DJPlaylistsModel.ssdl|res://*/DJPlaylistsModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source = DESKTOP - G63GCNR\\THEO;initial catalog = DJPlaylistDB; persist security info=True;user id = sa; password=@theo2017;MultipleActiveResultSets=True;App=EntityFramework\"";

        @"metadata=.\DJPlaylistsModel.csdl|.\DJPlaylistsModel.ssdl|.\DJPlaylistsModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-G63GCNR\THEO;initial catalog=DJPlaylistDB;persist security info=True;user id=sa;password=@theo2017;MultipleActiveResultSets=True;App=EntityFramework&quot;"; //providerName="System.Data.EntityClient"
        public static DJPlaylistEntities GetContext()
        {
            //return new DJPlaylistEntities(connectionString);
            return new DJPlaylistEntities();
        }
    }

    public partial class DJPlaylistEntities
    {
        
        public DJPlaylistEntities(string connectionString = null):base()
        {
            //EntityConnection entityConnection = new EntityConnection(connectionString);
            //entityConnection.Open();
            //this.Database.Connection.en
            //this.Database.Connection.ConnectionString = connectionString;
        }
    }
}
