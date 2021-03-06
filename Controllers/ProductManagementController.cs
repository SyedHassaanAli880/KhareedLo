using KhareedLo.Auth;
using KhareedLo.Models;
using KhareedLo.Repositories.Interfaces;
using KhareedLo.ViewModel;
using KhareedLo.ViewModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo.Controllers
{
    //[Authorize(Roles = "Administrators")]
    //[Authorize(Policy = "DeleteProduct")]
    public class ProductManagementController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly ICategoryRepository _categoryRepository;

        private readonly Interface<Product> _repository;

        private readonly AppDbContext _appdbcontext;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IHostingEnvironment _env;

        private readonly AppDbContext _db;

        public ProductManagementController(IProductRepository productRepository, UserManager<ApplicationUser> um, AppDbContext apdb, IHostingEnvironment env, Interface<Product> Repository, AppDbContext appDbContext, ICategoryRepository categoryRepository)
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
            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _productRepository.GetAllProducts().OrderBy(p => p.Name);

            foreach (var b in pproducts)
            {
                ProductViewModel product = new ProductViewModel
                {
                    Title = "PRODUCTS",
                    Id = b.Id,
                    Name = b.Name,
                    ImagePhoto = b.ImagePhoto,
                    Price = b.Price,
                    IsInStock = b.IsInStock,
                    Quantity = b.Quantity,
                    ShortDescription = b.ShortDescription,
                    LongDescription = b.LongDescription,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name

                };
                model.Add(product);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.categories = new SelectList(_db.CategoryModels, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel vari)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (vari.ImagePhoto != null)
                {
                    string uploadsfolder = Path.Combine(_env.WebRootPath, "images");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vari.ImagePhoto.FileName;

                    string filePath = Path.Combine(uploadsfolder, uniqueFileName);

                    await vari.ImagePhoto.CopyToAsync(new FileStream(filePath, FileMode.Create));

                }

                Product p = new Product()
                {
                    Name = vari.Name,
                    ShortDescription = vari.ShortDescription,
                    LongDescription = vari.LongDescription,
                    Price = vari.Price,
                    IsInStock = vari.IsInStock,
                    Quantity = vari.quantity,
                    ImagePhoto = uniqueFileName,
                    Category = vari.CategoryName,
                    CategoryId = vari.CatID
                   
                };
                p.ImagePhoto = uniqueFileName;
                int x = _productRepository.AddProduct(p);

                if(x > 0) //success
                {
                    TempData["prodID"] = p.Id;

                    return RedirectToAction("HomeListOfProducts", "ProductManagement");
                }
                else //failure
                {
                    return RedirectToAction("HomeListOfProducts", "ProductManagement");
                }

                
            }
            else
            {
                return View(vari);
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

            ViewBag.categories = new SelectList(_db.CategoryModels, "Id", "Name");

            if (product == null) return RedirectToAction("HomeListOfProducts","ProductManagement");

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductDetails(int id, Product vari, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Product prod = _appdbcontext.Products.Where(x => x.Id == id).FirstOrDefault();

                string currentImageName = prod.ImagePhoto;

                if (prod == null) return NotFound();

                if(file == null) //use existing image
                {
                    prod.Name = vari.Name;
                    prod.IsInStock = vari.IsInStock;
                    prod.LongDescription = vari.LongDescription;
                    prod.ShortDescription = vari.ShortDescription;
                    prod.Price = vari.Price;
                    prod.Quantity = vari.Quantity;
                    prod.Category = vari.Category;
                    prod.CategoryId = vari.CategoryId;
                    prod.ImagePhoto = currentImageName;
                    
                    _appdbcontext.SaveChanges();
                    
                    return RedirectToAction("HomeListOfProducts", "ProductManagement");
                }
                else //use new image 
                {
                    string fileName = Guid.NewGuid().ToString()+file.FileName+".jpg";
                    
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    prod.Name = vari.Name;
                    prod.IsInStock = vari.IsInStock;
                    prod.LongDescription = vari.LongDescription;
                    prod.ShortDescription = vari.ShortDescription;
                    prod.Price = vari.Price;
                    prod.Quantity = vari.Quantity;
                    prod.Category = vari.Category;
                    prod.CategoryId = vari.CategoryId;
                    prod.ImagePhoto = fileName;

                    _appdbcontext.SaveChanges();

                    return RedirectToAction("HomeListOfProducts", "ProductManagement");
                }
            }
            else
            {
                return View(vari);
            }
        }

        //[HttpGet]
        //public IActionResult AddCategoryToProduct(int prodId)
        //{
        //    int ID = Convert.ToInt32(TempData["prodID"]);

        //    var prod = _productRepository.GetProductById(ID);

        //    if (prod == null)
        //    {
        //        RedirectToAction("ProductManagement", "HomeListOfProducts");

        //    }

        //    var obj = new ProductCategoryViewModel { ProductId = ID };

        //    foreach (var categs in _categoryRepository.GGetAllCategories())
        //    {   obj.categories.Add(categs);              
        //    }

        //    return View(obj);
        //}

        //[HttpPost]
        //public IActionResult AddCategoryToProduct(ProductCategoryViewModel vari)
        //{
        //    int ID = Convert.ToInt32(TempData["prodID"]);   

        //    var product = _productRepository.GetProductById(vari.ProductId);

        //   var cat = _categoryRepository.GGetCategoryById(Convert.ToInt32(vari.CategoryName));

        //    product.Name = product.Name;
        //    product.ShortDescription = product.ShortDescription;
        //    product.LongDescription = product.LongDescription;
        //    product.Price = product.Price;
        //    product.IsInStock = product.IsInStock;
        //    product.Quantity = product.Quantity;
        //    product.ImagePhoto = product.ImagePhoto;
        //    //product.Category = cat.Name;

        //    if(_db.SaveChanges() > 0)
        //    {

        //        TempData["IsSuccess"] = true;

        //        return RedirectToAction("HomeListOfProducts", "ProductManagement");

        //    }
        //    else
        //    {
        //        TempData["IsSuccess"] = true;
        //        return RedirectToAction("AddCategoryToProduct", "ProductManagement");

        //    }
        //}
    }
}
