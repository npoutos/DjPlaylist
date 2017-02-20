using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class USER
    {
        public USER(string firstName,string lastName, string email)
        {
            FIRST_NAME = firstName;
            LAST_NAME = lastName;
            EMAIL = email;
            DATE_CREATED = DateTime.Now;
        }



    }
}
