using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using KhareedLo.Repositories;
using Microsoft.AspNetCore.Identity;
using KhareedLo.Repositories.Interfaces;
using KhareedLo.Models;
using KhareedLo.ViewModel.Category;
using KhareedLo.Models.Category;
using KhareedLo.ViewModel;

namespace KhareedLo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IProductRepository _productRepository;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppDbContext _db;

        public CategoryController(ICategoryRepository categoryRepository, UserManager<IdentityUser> um, AppDbContext apdb, IProductRepository obj)
        {
            _categoryRepository = categoryRepository;

            _userManager = um;

            _db = apdb;

            _productRepository = obj;
        }
        
        public async Task<IActionResult> DisplayCategories()
        {
            var ccategories = await _categoryRepository.GetAllCategories();

            var obj = new CategoryViewModel()
            {
                cvmcategories = ccategories.ToList()
            };

            return View(obj);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel vari)
        {
                CategoryModel p = new CategoryModel
                {
                    Name = vari.Name,
                    IsActive = vari.IsActive

                };
                
                var x = _categoryRepository.AddCategory(p);

                if (x != null) //success
                {
                    return RedirectToAction("DisplayCategories", "Category");
                }
                else //failure
                {
                    return RedirectToAction("AddCategory", "Category");
                }

        }

        [HttpPost]
        public IActionResult DeleteCategory(int ID)
        {
            bool result = _categoryRepository.DDeleteCategory(ID);

            if (result) //success
            {
                return RedirectToAction("DisplayCategories", "Category");
            }
            else //failure
            {
                return RedirectToAction("DisplayCategories", "Category");
            }

        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _categoryRepository.GGetCategoryById(id);

            if (category == null) return RedirectToAction("DisplayCategories", "Category");

            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(int id, CategoryModel vari)
        {
            if (ModelState.IsValid)
            {
                CategoryModel categ = _db.CategoryModels.Where(x => x.Id == id).FirstOrDefault();

                if (categ == null) return NotFound();

                try
                {
                    categ.Name = vari.Name;
                    categ.IsActive = vari.IsActive;

                    _db.SaveChanges();

                    return RedirectToAction("DisplayCategories", "Category");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }

            }
            else
            {
                return RedirectToAction("DisplayCategories", "Category");
            }
        }

        [HttpGet]
        public IActionResult LaptopProductsPage()
        {
            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            var obj = new ProductViewModel()
            {
                Title = "Products Shop",

                Products = pproducts.ToList()
            };

            return View(obj);
        }

        [HttpGet]
        public IActionResult AllInOnePCsPage()
        {
            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            var obj = new ProductViewModel()
            {
                Title = "Products Shop",

                Products = pproducts.ToList()
            };

            return View(obj);
        }

        [HttpGet]
        public IActionResult BakeryItemsPage()
        {

            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            var obj = new ProductViewModel()
            {
                Title = "Products Shop",

                Products = pproducts.ToList()
            };

            return View(obj);
        }

        [HttpGet]
        public IActionResult BeautyCreamsPage()
        {

            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            var obj = new ProductViewModel()
            {
                Title = "Products Shop",

                Products = pproducts.ToList()
            };

            return View(obj);
        }

        [HttpGet]
        public IActionResult ComputerCompsPage()
        {
            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            var obj = new ProductViewModel()
            {
                Title = "Products Shop",

                Products = pproducts.ToList()
            };

            return View(obj);
        }

        [HttpGet]
        public IActionResult ClothesPage()
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
