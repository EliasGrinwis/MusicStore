using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;
using System.Xml.Linq;

namespace MusicStore.Components
{
    [ViewComponent(Name = "CartSummary")]
    public class CartSummaryComponent : ViewComponent
    {
        private readonly StoreContext _context;

        public CartSummaryComponent(StoreContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = new ShoppingCart(HttpContext, _context);

            ViewData["CartCount"] = cart.GetCount();

            return View();
        }
    }
}
