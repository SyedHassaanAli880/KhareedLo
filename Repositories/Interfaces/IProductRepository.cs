using KhareedLo.Models;
using System.Collections.Generic;

namespace KhareedLo
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        List<Product> GetAllProductsByID(List<int> IDs);

        List<Product> GetAllProduct();

        Product GetProductById(int prodId);

        void DDeleteProduct(int ID);

        int AddProduct(Product vari);

        Product UpdateProduct(int id, Product vari);

        Product UpdateProductTT(Product vari);

        void DeleteProduct(int ID);
    }
}
