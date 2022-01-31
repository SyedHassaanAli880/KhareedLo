using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KhareedLo.Models
{
    public class ProductRepository:IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Product> GetAllProductsByID(List<int> IDs)
        {
            return _appDbContext.Products.Where(p => IDs.Contains(p.Id)).ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _appDbContext.Products.Include(c => c.Category).ToList();
        }

        public Product GetProductById(int prodId)
        {
            return _appDbContext.Products.FirstOrDefault(p => p.Id == prodId);
        }

        public int AddProduct(Product vari)
        {
            _appDbContext.Products.Add(vari);

            int x = _appDbContext.SaveChanges();

            return x;
        }

        public Product UpdateProduct(int id, Product obj)
        {  
            var prod = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            
            if (prod != null)
            {
                if (/*ModelState.IsValid*/true)
                {
                    //string uniqueFileName = null;

                    //if (vari.ImagePhoto != null)
                    //{
                    //    string uploadsfolder = Path.Combine(_env.WebRootPath, "images");

                    //    uniqueFileName = Guid.NewGuid().ToString() + "_" + vari.ImagePhoto.FileName;

                    //    string filePath = Path.Combine(uploadsfolder, uniqueFileName);

                    //    vari.ImagePhoto.CopyTo(new FileStream(filePath, FileMode.Create));

                    //}


                    prod.Name = obj.Name;
                    prod.ShortDescription = obj.ShortDescription;
                    prod.LongDescription = obj.LongDescription;
                    prod.Price = obj.Price;
                    prod.IsInStock = obj.IsInStock;
                    prod.Quantity = obj.Quantity;

                    //ImagePhoto = uniqueFileName


                    //Products p = new Products
                    //{
                    //    Name = vari.Name,
                    //    ShortDescription = vari.ShortDescription,
                    //    LongDescription = vari.LongDescription,
                    //    Price = vari.Price,
                    //    IsInStock = vari.IsInStock,
                    //    Quantity = vari.Quantity,

                    //    //ImagePhoto = uniqueFileName
                    //};

                    _appDbContext.SaveChanges();
                }
            }

            return prod;
        }

        public void DeleteProduct(int ID)
        {
            var prod = _appDbContext.Products.FirstOrDefault(x=>x.Id == ID);
            
            if(prod != null)
            {
                _appDbContext.Remove(prod);

                _appDbContext.SaveChanges();
            }
        }
    }
}
