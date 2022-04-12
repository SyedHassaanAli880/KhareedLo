using KhareedLo.Models;
using KhareedLo.ViewModel;
using KhareedLo.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using KhareedLo.Repositories.Interfaces;
using KhareedLo.Repositories;

namespace KhareedLo.Controllers
{
    public class CartController : Controller
    {
        private readonly IGenericRepository<Product> _genericRep;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AppDbContext _db;
        public CartController(IGenericRepository<Product> _grp, UserManager<ApplicationUser> um, AppDbContext apdb)
        {
            _genericRep = _grp;

            _userManager = um;

            _db = apdb;
        }

        [HttpGet]
        public IActionResult CartProducts()
        {
            CheckOutViewModel model = new CheckOutViewModel();

            var cartprods = Request.Cookies["cartprods"];
            
            if(cartprods != null)
            {
                var prodIDs = cartprods.Split("-");

                model.CartProductIDs = prodIDs.Select(x => int.Parse(x)).ToList();

                model.CartProducts = _genericRep.GetAllProductsById(model.CartProductIDs);

                //model.CartProducts = _genericRep.GetAllProducts
            }

            return View(model);
        }

        List<int> obj = new List<int>();

        [HttpPost]
        public IActionResult Index(int ID)
        {
            obj.Add(ID);

            var product = _genericRep.GetById(ID);

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

            //int x = _genericRep.AddProduct(adt);

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
