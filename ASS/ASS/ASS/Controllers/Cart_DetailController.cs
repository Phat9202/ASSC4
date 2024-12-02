using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASS.Data;
using ASS.Models;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace ASS.Controllers
{
    public class Cart_DetailController : Controller
    {
        private readonly MyDb _context;

        public Cart_DetailController(MyDb context)
        {
            _context = context;
        }

        // GET: Cart_Detail
        public async Task<IActionResult> Index()
        {
            int? accountId = HttpContext.Session.GetInt32("AccountId");

            if (accountId == null)
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập hoặc thông báo lỗi
                return RedirectToAction("Login", "Account");
            }

            // Lấy Cart của người dùng dựa trên AccountId
            var cart = _context.Carts
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Product)
                .FirstOrDefault(c => c.AccountId == accountId);

            if (cart == null)
            {
                return View(new List<Cart_Detail>()); // Trả về danh sách trống nếu không có giỏ hàng
            }

            var cartDetails = cart.CartDetails.ToList();

            return View(cartDetails);
        }

        // GET: Cart_Detail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart_Detail = await _context.Cart_Details
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart_Detail == null)
            {
                return NotFound();
            }

            return View(cart_Detail);
        }

        // POST: Cart_Detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        // GET: Cart_Detail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart_Detail = await _context.Cart_Details.FindAsync(id);
            if (cart_Detail == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", cart_Detail.CartId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cart_Detail.ProductId);
            return View(cart_Detail);
        }

        // POST: Cart_Detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CartId,ProductId,Price,Quantity,Total")] Cart_Detail cart_Detail)
        {
            if (id != cart_Detail.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart_Detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Cart_DetailExists(cart_Detail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", cart_Detail.CartId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cart_Detail.ProductId);
            return View(cart_Detail);
        }

        // GET: Cart_Detail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart_Detail = await _context.Cart_Details
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart_Detail == null)
            {
                return NotFound();
            }

            return View(cart_Detail);
        }

        // POST: Cart_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartDetail = await _context.Cart_Details.FindAsync(id);
            if (cartDetail == null)
            {
                return Json(new { success = false, message = "Chi tiết giỏ hàng không tồn tại." });
            }

            var cartId = cartDetail.CartId;
            _context.Cart_Details.Remove(cartDetail);
            await _context.SaveChangesAsync();

            // Cập nhật TotalPrice cho Cart
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
            {
                cart.TotalPrice = _context.Cart_Details
                    .Where(cd => cd.CartId == cartId)
                    .Sum(cd => cd.Total);

                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        private bool Cart_DetailExists(int id)
        {
            return _context.Cart_Details.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int id, int quantity, decimal totalPrice)
        {
            var cartDetail = _context.Cart_Details.Find(id);
            if (cartDetail == null)
            {
                return Json(new { success = false, message = "Chi tiết giỏ hàng không tồn tại." + id});
            }

            var product = _context.Products.Find(cartDetail.ProductId);
            if (product == null || quantity > product.SoLuong)
            {
                return Json(new { success = false, message = "Số lượng không hợp lệ." });
            }

            cartDetail.Quantity = quantity;
            cartDetail.Total = quantity* cartDetail.Price;
            _context.SaveChanges();

            var cart = _context.Carts.Include(c => c.CartDetails).FirstOrDefault(c => c.Id == cartDetail.CartId);
            if (cart != null)
            {
                cart.TotalPrice = cart.CartDetails.Sum(cd => cd.Total);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SaveCartToSession(List<Cart_Detail> cartItems)
        {
            var cartItemsJson = JsonConvert.SerializeObject(cartItems);
            HttpContext.Session.SetString("CartItems", cartItemsJson);
            return Ok();
        }
    }
}
