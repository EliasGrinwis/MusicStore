using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Album
    {
        public int AlbumID { get; set; }
        public int GenreID { get; set; }
        public int ArtistID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string AlbumArtUrl { get; set; }

        public Genre? Genre { get; set; }
        public Artist? Artist { get; set; }
    }
}
