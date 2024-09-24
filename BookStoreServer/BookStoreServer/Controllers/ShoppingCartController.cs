using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using WebApplication15.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication15.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // Ensure only logged-in users can manage the cart
    public class ShoppingCartController : ControllerBase
    {
        private readonly BooksStoreContext _context;

        public ShoppingCartController(BooksStoreContext context)
        {
            _context = context;
        }

        // POST: api/shoppingcart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(long bookId, int quantity)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var userId = GetUserId(); // Assuming you have a method to get the current user's ID

            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == userId);

            if (cart == null)
            {
                cart = new ShoppingCart { Id = userId };
                _context.ShoppingCarts.Add(cart);
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem {BookId = bookId, Quantity = quantity });
            }

            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        private int GetUserId()
        {
            // Logic to get the current logged-in user's ID
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("placeorder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = 1;// GetUserId(); // Retrieve the current user's ID

            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.Id == userId);

            if (cart == null || !cart.Items.Any())
            {
                return BadRequest("Your cart is empty.");
            }

            // Calculate the total amount
            double totalAmount = cart.Items.Sum(i => i.Quantity * i.Book.Price);

            // Create the order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                OrderItems = cart.Items.Select(i => new OrderItem
                {
                    BookId = i.BookId,
                    Quantity = i.Quantity,
                    UnitPrice = i.Book.Price
                }).ToList()
            };

            _context.Orders.Add(order);

            // Clear the cart
            _context.CartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();

            // Simulate payment processing (this can be replaced with actual payment gateway logic)
            bool paymentSuccessful = SimulatePaymentProcessing(totalAmount);

            if (!paymentSuccessful)
            {
                return BadRequest("Payment failed. Please try again.");
            }

            return Ok(order);
        }

        private bool SimulatePaymentProcessing(double totalAmount)
        {
            // Simulate payment logic
            return true; // Simulate successful payment
        }



    }

}
