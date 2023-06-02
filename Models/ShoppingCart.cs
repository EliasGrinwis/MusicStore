using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class ShoppingCart
    {
        private StoreContext _context;
        string _cartKey { get; set; }
        
        public ShoppingCart (HttpContext httpContext, StoreContext context)
        {
            _cartKey = GetCartKey(httpContext);
            _context = context;
        }

        private string GetCartKey(HttpContext httpContext)
        {
            if (httpContext.Session.GetString("CartKey") == null)
            {
                Guid tempCartKey = Guid.NewGuid();
                httpContext.Session.SetString("CartKey", tempCartKey.ToString());
            }
            return httpContext.Session.GetString("CartKey");
        }

        public void AddToCart(Album album)
        {
            // Get the matching cart and album instances
            var cartItem = _context.CartItems.SingleOrDefault(
                c => c.CartKey == _cartKey
                && c.AlbumID == album.AlbumID);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                    AlbumID = album.AlbumID,
                    CartKey = _cartKey,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            _context.SaveChanges();
        }
        public void RemoveFromCart(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }

        public void SubstractOneFromCart(CartItem cartItem)
        {

            if (cartItem.Count < 2)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
            else
            {
                cartItem.Count--;

                _context.Update(cartItem);
                _context.SaveChanges();
            }
        }

        public void AddOneToCart(CartItem cartItem)
        {
            cartItem.Count++;

            _context.Update(cartItem);
            _context.SaveChanges();
        }


        public void EmptyCart()
        {
            var cartItems = _context.CartItems.Where(
                c => c.CartKey == _cartKey);

            foreach (var cartItem in cartItems)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            var cartItems = _context.CartItems.Include(c => c.Album).ThenInclude(c => c.Artist).Where(
                c => c.CartKey == _cartKey);
                
            return cartItems.ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from c in _context.CartItems
                          where c.CartKey == _cartKey
                          select (int?)c.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public void CreateOrderItems(Order order)
        {
            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)  {
                var orderItem = new OrderItem {
                    AlbumID = item.AlbumID,
                    OrderID = order.OrderID,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                _context.OrderItems.Add(orderItem);
            }
            
            // Save the orderitems
            _context.SaveChanges();
            // Empty the shopping cart
            EmptyCart();

            return;
        }
                
    }
}
