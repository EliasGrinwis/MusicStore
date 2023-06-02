using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public string CartKey { get; set; }
        public int AlbumID { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public Album? Album { get; set; }
    }
}
