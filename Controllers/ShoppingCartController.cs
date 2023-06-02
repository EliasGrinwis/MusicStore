using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly StoreContext _context;

        public ShoppingCartController(StoreContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartItems = _context.CartItems.Include(c => c.Album);

            return View(cartItems.ToList());
        }

        public IActionResult AddToCart(int id) 
        {
            var album = _context.Albums.Find(id);

            var shoppingCart = new ShoppingCart(HttpContext, _context);

            shoppingCart.AddToCart(album);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            // Retrieve the cartItem from the database using its ID
            var cartItem = _context.CartItems.Find(id);

            var shoppingCart = new ShoppingCart(HttpContext, _context);

            shoppingCart.RemoveFromCart(cartItem);

            // Redirect to the Index action
            return RedirectToAction("Index");
        }

        public IActionResult SubstractOneFromCart(int id)
        {
            // Retrieve the cartItem from the database using its ID
            var cartItem = _context.CartItems.Find(id);

            var shoppingCart = new ShoppingCart(HttpContext, _context);

            shoppingCart.SubstractOneFromCart(cartItem);

            return RedirectToAction("Index");
        }

        public IActionResult AddOneToCart(int id)
        {
            // Retrieve the cartItem from the database using its ID
            var cartItem = _context.CartItems.Find(id);

            var shoppingCart = new ShoppingCart(HttpContext, _context);

            shoppingCart.AddOneToCart(cartItem);

            return RedirectToAction("Index");
        }
    }
}
