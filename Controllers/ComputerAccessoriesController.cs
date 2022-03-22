using KhareedLo.Auth;
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

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AppDbContext _db;

        public ComputerAccessoriesController(IProductRepository productRepository, UserManager<ApplicationUser> um, AppDbContext apdb)
        {
            _productRepository = productRepository;

            _userManager = um;

            _db = apdb;
        }
        public IActionResult ComputerAccessoriesDisplayPage()
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
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name

                };
                model.Add(product);
            }

            return View(model);

        }
    }
}
