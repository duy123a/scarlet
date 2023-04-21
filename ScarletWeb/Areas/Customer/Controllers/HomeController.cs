using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scarlet.DataAccess.Repository.IRepository;
using Scarlet.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ScarletWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                shoppingCart.ApplicationUserId = userId!;

                ShoppingCart? cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId);

                if (cartFromDb != null)
                {
                    // shopping car exist
                    cartFromDb.Count += shoppingCart.Count;
                    _unitOfWork.ShoppingCart.Update(cartFromDb);
                }
                else
                {
                    // add cart record
                    _unitOfWork.ShoppingCart.Add(shoppingCart);
                }
                TempData["success"] = "Cart updated successfully";
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            else
            {
                shoppingCart.Product = _unitOfWork.Product.Get(u => u.Id == shoppingCart.ProductId, includeProperties: "Category");
                return View(shoppingCart);
            }
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
    }
}