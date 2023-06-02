using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicStore.Models.ViewModels
{
    public class ListGenreArtistVM
    {
        public List<Album> Albums;
        public SelectList Genres { get; set; }
        public SelectList Artists { get; set; }
        public int genreID { get; set; }
        public int artistID { get; set; }
        public string title { get; set; }
    }
}
