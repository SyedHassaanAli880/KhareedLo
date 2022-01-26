using KhareedLo.Models;
using KhareedLo.Models.Category;
using KhareedLo.Repositories.Interfaces;
using KhareedLo.ViewModel;
using KhareedLo.ViewModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace KhareedLo.Controllers
{
    //[Authorize(Roles = "Administrators")]
    //[Authorize(Policy = "DeleteProduct")]
    public class ProductManagementController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly ICategoryRepository _categoryRepository;

        private readonly Interface<Products> _repository;

        private readonly AppDbContext _appdbcontext;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IHostingEnvironment _env;

        private readonly AppDbContext _db;

        public ProductManagementController(IProductRepository productRepository, UserManager<IdentityUser> um, AppDbContext apdb, IHostingEnvironment env, Interface<Products> Repository, AppDbContext appDbContext, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;

            _userManager = um;

            _db = apdb;

            _env = env;

            _repository = Repository;

            _appdbcontext = appDbContext;

            _categoryRepository = categoryRepository;
        }

        public IActionResult HomeListOfProducts()
        {
            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            var obj = new ProductViewModel()
            {
                Title = "Product Shop",

                Products = pproducts.ToList()
            };

            return View(obj);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.categories = _categoryRepository.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductViewModel vari)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (vari.ImagePhoto != null)
                {
                    string uploadsfolder = Path.Combine(_env.WebRootPath, "images");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vari.ImagePhoto.FileName;

                    string filePath = Path.Combine(uploadsfolder, uniqueFileName);

                    vari.ImagePhoto.CopyTo(new FileStream(filePath, FileMode.Create));

                }

                Products p = new Products()
                {
                    Name = vari.Name,
                    ShortDescription = vari.ShortDescription,
                    LongDescription = vari.LongDescription,
                    Price = vari.Price,
                    IsInStock = vari.IsInStock,
                    Quantity = vari.quantity,
                    ImagePhoto = uniqueFileName
                };

                int x = _productRepository.AddProduct(p);

                if(x > 0) //success
                {
                    TempData["prodID"] = p.Id;

                    return RedirectToAction("AddCategoryToProduct", "ProductManagement");
                }
                else //failure
                {
                    return RedirectToAction("HomeListOfProducts", "ProductManagement");
                }

                
            }
            else
            {
                return RedirectToAction("HomeListOfProducts", "ProductManagement");
            }
           
        }

        [HttpPost]
        public IActionResult DeleteProduct(int ID)
        {
            _productRepository.DeleteProduct(ID);

            return RedirectToAction("HomeListOfProducts", "ProductManagement");
        }

        [HttpGet]
        public IActionResult EditProductDetails(int id)
        {
            var product =  _repository.GetById(id);

            if (product == null) return RedirectToAction("HomeListOfProducts","ProductManagement");

            return View(product);
        }

        [HttpPost]
        public IActionResult EditProductDetails(int id ,Products vari/*, IFormFile file*/)
        {
            if (ModelState.IsValid)
            {
                Products prod = _appdbcontext.Products.Where(x=>x.Id == id).FirstOrDefault();

                if (prod == null) return NotFound();

                try
                {
                    if (/*file != null*/false)
                    {
                        //string filename = System.Guid.NewGuid().ToString() + ".jpg";

                        //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

                        //using (var stream = new FileStream(path, FileMode.Create))
                        //{
                        //    file.CopyToAsync(stream);
                        //}

                        prod.Name = vari.Name;
                        prod.IsInStock = vari.IsInStock;
                        prod.LongDescription = vari.LongDescription;
                        prod.ShortDescription = vari.ShortDescription;
                        prod.Price = vari.Price;
                        prod.Quantity = vari.Quantity;
                        //prod.ImagePhoto = filename;
                        _appdbcontext.SaveChanges();
                        return RedirectToAction("HomeListOfProducts", "ProductManagement");

                    }
                    else
                    {

                        prod.Name = vari.Name;
                        prod.IsInStock = vari.IsInStock;
                        prod.LongDescription = vari.LongDescription;
                        prod.ShortDescription = vari.ShortDescription;
                        prod.Price = vari.Price;
                        prod.Quantity = vari.Quantity;
                        //prod.ImagePhoto = vari.ImagePhoto;
                        _appdbcontext.SaveChanges();
                        return RedirectToAction("HomeListOfProducts", "ProductManagement");

                        
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message); 
                   
                }

            }
            else
            {
                return RedirectToAction("HomeListOfProducts", "ProductManagement");
            }
        }

        [HttpGet]
        public IActionResult AddCategoryToProduct(int prodId)
        {
            int ID = Convert.ToInt32(TempData["prodID"]);

            var prod = _productRepository.GetProductById(ID);

            if (prod == null)
            {
                RedirectToAction("ProductManagement", "HomeListOfProducts");
                
            }

            var obj = new ProductCategoryViewModel { ProductId = ID };

            foreach (var categs in _categoryRepository.GGetAllCategories())
            {   obj.categories.Add(categs);              
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult AddCategoryToProduct(ProductCategoryViewModel vari)
        {
            int ID = Convert.ToInt32(TempData["prodID"]);   

            var product = _productRepository.GetProductById(vari.ProductId);

           var cat = _categoryRepository.GGetCategoryById(Convert.ToInt32(vari.CategoryName));

            product.Name = product.Name;
            product.ShortDescription = product.ShortDescription;
            product.LongDescription = product.LongDescription;
            product.Price = product.Price;
            product.IsInStock = product.IsInStock;
            product.Quantity = product.Quantity;
            product.ImagePhoto = product.ImagePhoto;
            product.Category = cat.Name;

            if(_db.SaveChanges() > 0)
            {

                TempData["IsSuccess"] = true;

                return RedirectToAction("HomeListOfProducts", "ProductManagement");

            }
            else
            {
                TempData["IsSuccess"] = true;
                return RedirectToAction("AddCategoryToProduct", "ProductManagement");

            }
        }
    }
}
