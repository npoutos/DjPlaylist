using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJPlaylistsModel
{
    public partial class GENRE
    {
        public GENRE(string description)
        {
            DESCRIPTION = description.Trim().ToUpper();
        }
    }
}
