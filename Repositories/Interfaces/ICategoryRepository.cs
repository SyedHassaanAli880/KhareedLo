using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhareedLo.Models;

namespace KhareedLo.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllCategories();

        IEnumerable<CategoryModel> GGetAllCategories();

        Task<IEnumerable<CategoryModel>> Search(string name);

        Task<CategoryModel> GetCategoryById(int prodId);

        CategoryModel GGetCategoryById(int prodId);

        Task<CategoryModel> AddCategory(CategoryModel vari);

        Task<CategoryModel> UpdateCategory(int id, CategoryModel vari);

        Task DeleteCategory(int ID);

        bool DDeleteCategory(int ID);
    }
}
