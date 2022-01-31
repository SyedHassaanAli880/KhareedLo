using KhareedLo.Models;
using System.Collections.Generic;

namespace KhareedLo
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        List<Product> GetAllProductsByID(List<int> IDs);

        Product GetProductById(int prodId);

        int AddProduct(Product vari);

        Product UpdateProduct(int id, Product vari);

        void DeleteProduct(int ID);
    }
}
