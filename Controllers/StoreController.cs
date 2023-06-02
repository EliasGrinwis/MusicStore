using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;
        public StoreController(StoreContext context) 
        {
            _context = context;
        }

        // GET: Admin/Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        public IActionResult ListGenres()
        {
            var genres = _context.Genres.OrderBy(g => g.Name).ToList();

            return View(genres);
        }

        public IActionResult ListAlbums(int id)
        {
            var albums = _context.Albums.Where(a => a.GenreID == id).ToList();

            return View(albums);
        }

        public IActionResult AddToCart(int id)
        {

            return View();
        }
    }
}
