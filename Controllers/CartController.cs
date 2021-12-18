using KhareedLo.Models;
using KhareedLo.Models.cart;
using KhareedLo.Repositories;
using KhareedLo.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using KhareedLo.Models.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppDbContext _db;


        [HttpGet]
        public IActionResult CartProducts()
        {
            CheckOutViewModel model = new CheckOutViewModel();

            var cartprods = Request.Cookies["cartprods"];
            
            if(cartprods != null)
            {
                var prodIDs = cartprods.Split("-");

                model.CartProductIDs = prodIDs.Select(x => int.Parse(x)).ToList();

                model.CartProducts = _productRepository.GetAllProductsByID(model.CartProductIDs);

                //model.CartProducts = _productRepository.GetAllProducts
            }

            return View(model);
        }

        public CartController(IProductRepository productRepository, UserManager<IdentityUser> um, AppDbContext apdb)
        {
            _productRepository = productRepository;

            _userManager = um;

            _db = apdb;
        }

        List<int> obj = new List<int>();

        [HttpPost]
        public IActionResult Index(int ID)
        {
            obj.Add(ID);

            var product = _productRepository.GetProductById(ID);

            AddToCart adt = new AddToCart()
            {
                Name = product.Name,
                Price = Convert.ToInt32(product.Price),
                ProdId = ID
                //Quantity = 
                //IsInStock = vari.IsInStock,
                //quantity = vari.quantity,
                //ImagePhoto = uniqueFileName
            };

            //int x = _productRepository.AddProduct(adt);

            //string key = "CartProducts";

            //int value = ID;

            //ViewBag.PRODid = ID;

            //CookieOptions cookieOptions = new CookieOptions();

            //cookieOptions.Expires = DateTime.Now.AddDays(7);

            //Response.Cookies.Append(key, value.ToString(), cookieOptions);

            return RedirectToAction("ShowID", "Cart");
        }

        [HttpGet]
        public IActionResult ShowID()
        {
            return View("Index","Cart");
        }

        [HttpGet]
        public IActionResult CheckOutPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOutPagge()
        {
            TempData["OrderPlaced"] = true;

            return RedirectToAction("Index","Product");
        }

    }
}
