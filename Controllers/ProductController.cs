using KhareedLo.Models;
using KhareedLo.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KhareedLo.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppDbContext _db;

        public ProductController(IProductRepository productRepository, UserManager<IdentityUser> um, AppDbContext apdb)
        {
            _productRepository = productRepository;

            _userManager = um;

            _db = apdb;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            foreach (var b in pproducts)
            {
                ProductViewModel product = new ProductViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    ImagePhoto = b.ImagePhoto,
                    ShortDescription = b.ShortDescription
                    
                };
                model.Add(product);
            }

            return View(model);
        }

        public IActionResult Details(int ID)
        {
            var product = _productRepository.GetProductById(ID);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        int[] prodID;

        int i = 0;

        public IActionResult CreateIDArray(int Id)
        {
            prodID[i] = Id;
            ++i;
            return RedirectToAction("Index");
        }

        public IActionResult SaveProdCookie()
        {
            //var product = _productRepository.GetProductById(ID);

            //if (product == null)
            //{
            //    return NotFound();
            //}

            //prodID = Id;

            string key = "CartProducts";

            int[] value = prodID;

            CookieOptions cookieOptions = new CookieOptions();

            cookieOptions.Expires = DateTime.Now.AddDays(7);

            Response.Cookies.Append(key, value.ToString(), cookieOptions);

            return RedirectToAction("Index");
        }

        public IActionResult SearchProducts(string searchProduct)
        {
            if (string.IsNullOrEmpty(searchProduct))
            {
                //var product = _productRepository.GetProductById(ID);
                return View();
            }
            return View();
        }

        public IActionResult Read()
        {
            string key = "CartProducts";

            var cookievalue = Request.Cookies[key];

            var product = _productRepository.GetProductById(Convert.ToInt32(cookievalue));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
