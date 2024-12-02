using ASS.Data;
using ASS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly MyDb _context;
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger, MyDb context)
    {
        _context = context;
        _logger = logger;
    }
    public IActionResult Index()
    {
        var accountId = HttpContext.Session.GetInt32("AccountId");
        ViewBag.AccountId = accountId;
        if (accountId == null)
        {
            return RedirectToAction("Login", "Accounts");
        }
        var account = _context.Accounts.Find(accountId);
        return View(account); // Pass the account to the view
    }
    public IActionResult Privacy()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult IndexAdmin()
    {
        var accountId = HttpContext.Session.GetInt32("AccountId");
        if (accountId == null)
        {
            return RedirectToAction("Login", "Accounts");
        }
        var account = _context.Accounts.Find(accountId);
        return View(account); // Pass the account to the view
    }

    public async Task<IActionResult> IndexUser()
    {
        var accountId = HttpContext.Session.GetInt32("AccountId");
        ViewBag.AccountId = accountId;
        var myDb = _context.Products.Include(p => p.PhanLoai).Include(p => p.ThuongHieu);
        return View(await myDb.ToListAsync());
    }
    public async Task<IActionResult> Details(int? id)
    {
        var accountId = HttpContext.Session.GetInt32("AccountId");
        ViewBag.AccountId = accountId;
        if (id == null)
        {
            return NotFound();
        }

        var productModel = await _context.Products
            .Include(p => p.PhanLoai)
            .Include(p => p.ThuongHieu)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (productModel == null)
        {
            return NotFound();
        }

        return View(productModel);
    }

    [HttpPost]
    public IActionResult AddToCart(int ProductId, int Quantity, decimal Price)
    {
        // Lấy thông tin sản phẩm từ cơ sở dữ liệu
        var product = _context.Products.Find(ProductId);
        if (product == null)
        {
            // Nếu sản phẩm không tồn tại
            return NotFound("Sản phẩm không tồn tại.");
        }

        // Kiểm tra số lượng còn lại
        if (Quantity > product.SoLuong)
        {
            // Số lượng đặt vượt quá số lượng còn lại
            ModelState.AddModelError("", "Số lượng sản phẩm không đủ.");
            return RedirectToAction("Details", product); // Hiển thị lại trang chi tiết sản phẩm
        }

        // Giả sử bạn đã có thông tin người dùng và CartId
        int cartId = GetCartIdForUser();

        // Kiểm tra nếu Cart_Detail đã tồn tại với cùng CartId và ProductId
        var existingCartDetail = _context.Cart_Details
            .FirstOrDefault(cd => cd.CartId == cartId && cd.ProductId == ProductId);

        if (existingCartDetail != null)
        {
            // Tính toán tổng số lượng sau khi thêm
            int newQuantity = existingCartDetail.Quantity + Quantity;

            if (newQuantity > product.SoLuong)
            {
                // Tổng số lượng vượt quá số lượng hiện tại của sản phẩm
                ModelState.AddModelError("", "Tổng số lượng sản phẩm trong giỏ hàng vượt quá số lượng hiện có.");
                return RedirectToAction("Details", product); // Hiển thị lại trang chi tiết sản phẩm
            }

            // Cập nhật số lượng và tổng giá trị
            existingCartDetail.Quantity = newQuantity;
            existingCartDetail.Total = existingCartDetail.Price * newQuantity;
        }
        else
        {
            // Tạo mới Cart_Detail
            var cartDetail = new Cart_Detail
            {
                CartId = cartId,
                ProductId = ProductId,
                Quantity = Quantity,
                Price = Price,
                Total = Price * Quantity
            };

            _context.Cart_Details.Add(cartDetail);
        }

        // Cập nhật TotalPrice cho Cart
        var cart = _context.Carts.Find(cartId);
        if (cart != null)
        {
            cart.TotalPrice = _context.Cart_Details
                .Where(cd => cd.CartId == cartId)
                .Sum(cd => cd.Total);

            _context.SaveChanges();
        }

        // Lưu thay đổi vào cơ sở dữ liệu
        _context.SaveChanges();

        // Chuyển hướng về trang giỏ hàng hoặc trang nào đó
        return RedirectToAction("Index", "Cart_Detail");
    }
    private int GetCartIdForUser()
    {
        // Lấy AccountId từ session
        int? accountId = HttpContext.Session.GetInt32("AccountId");

        // Kiểm tra xem người dùng đã đăng nhập hay chưa
        if (accountId == null)
        {
            // Xử lý khi người dùng chưa đăng nhập (ví dụ: chuyển hướng đến trang đăng nhập)
            return 0; // Hoặc bất kỳ giá trị nào phù hợp với logic của bạn
        }

        // Kiểm tra xem đã có Cart nào cho người dùng này chưa
        var cart = _context.Carts
            .FirstOrDefault(c => c.AccountId == accountId);

        // Nếu không có Cart nào tồn tại, tạo mới
        if (cart == null)
        {
            cart = new Cart
            {
                AccountId = accountId.Value,
                TotalPrice = 0, // Khởi tạo giá trị tổng giá
                Status = false // Giả sử false là trạng thái của giỏ hàng đang hoạt động
            };

            _context.Carts.Add(cart);
            _context.SaveChanges(); // Lưu Cart mới và lấy Id của nó
        }

        return cart.Id;
    }


    [HttpPost]
    public IActionResult UpdateQuantity(int id, int quantity, decimal totalPrice)
    {
        var cartDetail = _context.Cart_Details.Find(id);
        if (cartDetail == null)
        {
            return Json(new { success = false, message = "Chi tiết giỏ hàng không tồn tại." });
        }

        var product = _context.Products.Find(cartDetail.ProductId);
        if (product == null)
        {
            return Json(new { success = false, message = "Sản phẩm không tồn tại." });
        }

        if (quantity < 1 || quantity > product.SoLuong)
        {
            return Json(new { success = false, message = $"Số lượng không hợp lệ. Số lượng tối đa hiện tại là {product.SoLuong}." });
        }

        // Cập nhật số lượng và tổng giá
        cartDetail.Quantity = quantity;
        cartDetail.Total = quantity * product.Gia; // Tính tổng giá dựa trên số lượng và giá sản phẩm
        _context.SaveChanges();

        return Json(new { success = true });
    }

    public IActionResult ThongTin()
    {
        var accountId = HttpContext.Session.GetInt32("AccountId");
        ViewBag.AccountId = accountId;
        return View();
    }
    public IActionResult BaiViet()
    {
        var accountId = HttpContext.Session.GetInt32("AccountId");
        ViewBag.AccountId = accountId;
        return View();
    }
    public IActionResult HoTro()
    {
        var accountId = HttpContext.Session.GetInt32("AccountId");
        ViewBag.AccountId = accountId;
        return View();
    }
}
