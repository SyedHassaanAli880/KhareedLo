using KhareedLo.Models;
using KhareedLo.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo.Controllers
{
    public class ComputerAccessoriesController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppDbContext _db;

        public ComputerAccessoriesController(IProductRepository productRepository, UserManager<IdentityUser> um, AppDbContext apdb)
        {
            _productRepository = productRepository;

            _userManager = um;

            _db = apdb;
        }
        public IActionResult ComputerAccessoriesDisplayPage()
        {
            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            var obj = new ProductViewModel()
            {
                Title = "Products Shop",

                Products = pproducts.ToList()
            };

            return View(obj);

        }
    }
}
