using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KhareedLo.Repositories.Interfaces;
using KhareedLo.Models;
using KhareedLo.ViewModel.Category;
using KhareedLo.ViewModel;
using System.Collections.Generic;
using KhareedLo.Auth;

namespace KhareedLo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<CategoryModel> _GRepository;

        private readonly IGenericRepository<Product> _PGRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AppDbContext _db;

        public CategoryController(UserManager<ApplicationUser> um, AppDbContext apdb, IProductRepository obj, IGenericRepository<CategoryModel> GG, IGenericRepository<Product> prodRep)
        {
            _userManager = um;

            _db = apdb;

            _PGRepository = prodRep;

            _GRepository = GG;
        }
        
        public IActionResult DisplayCategories()
        {
            var ccategories = _GRepository.GetAll();

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
        public IActionResult AddCategory(CategoryModel vari)
        {
            CategoryModel p = new CategoryModel();

                if (ModelState.IsValid)
                {
                    p.Name = vari.Name;
                    
                    p.IsActive = vari.IsActive;
                    
                    _GRepository.Insert(p);

                    //success
                    return RedirectToAction("DisplayCategories", "Category");
                }
                else
                {
                    return View(p);
                }
        }

        [HttpPost]
        public IActionResult DeleteCategory(int ID)
        {
            bool result = _GRepository.Delete(ID);

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
            var category = _GRepository.GetById(id);

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

                    categ.Name = vari.Name;
                
                    categ.IsActive = vari.IsActive;

                    _db.SaveChanges();

                    return RedirectToAction("DisplayCategories", "Category");
            }
            else
            {
                return View(vari);
            }
        }

        [HttpGet]
        public IActionResult LaptopProductsPage()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _PGRepository.GetAll().OrderBy(p => p.Name);

            foreach (var b in pproducts)
            {
                ProductViewModel product = new ProductViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    ImagePhoto = b.ImagePhoto,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name

                };
                model.Add(product);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AllInOnePCsPage()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _PGRepository.GetAll().OrderBy(p => p.Name);

            foreach (var b in pproducts)
            {
                ProductViewModel product = new ProductViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    ImagePhoto = b.ImagePhoto,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name

                };
                model.Add(product);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult BakeryItemsPage()
        {

            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _PGRepository.GetAll().OrderBy(p => p.Name);

            foreach (var b in pproducts)
            {
                ProductViewModel product = new ProductViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    ImagePhoto = b.ImagePhoto,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name

                };
                model.Add(product);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult BeautyCreamsPage()
        {

            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _PGRepository.GetAll().OrderBy(p => p.Name);

            foreach (var b in pproducts)
            {
                ProductViewModel product = new ProductViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    ImagePhoto = b.ImagePhoto,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name

                };
                model.Add(product);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ComputerCompsPage()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _PGRepository.GetAll().OrderBy(p => p.Name);

            foreach (var b in pproducts)
            {
                ProductViewModel product = new ProductViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    ImagePhoto = b.ImagePhoto,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name

                };
                model.Add(product);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ClothesPage()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _PGRepository.GetAll().OrderBy(p => p.Name);

            foreach (var b in pproducts)
            {
                ProductViewModel product = new ProductViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    ImagePhoto = b.ImagePhoto,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name

                };
                model.Add(product);
            }

            return View(model);
        }
    }
}
