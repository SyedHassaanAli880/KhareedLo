using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhareedLo.Repositories.Interfaces;
using KhareedLo.Models;
using Microsoft.EntityFrameworkCore;

namespace KhareedLo.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            return await _appDbContext.CategoryModels.ToListAsync();
        }

        public IEnumerable<CategoryModel> GGetAllCategories()
        {
            return _appDbContext.CategoryModels.OrderBy(p=>p.Id);
        }

        public async Task<IEnumerable<CategoryModel>> Search(string name)
        {
            IQueryable<CategoryModel> query = _appDbContext.CategoryModels;

            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<CategoryModel> GetCategoryById(int prodId)
        {
            return await _appDbContext.CategoryModels.FirstOrDefaultAsync(p => p.Id == prodId);

        }

        public CategoryModel GGetCategoryById(int prodId)
        {
            var result = _appDbContext.CategoryModels.FirstOrDefault(p => p.Id == prodId);

            //if(result != null) //category found
            //{
                return result;
            //}
         
        }

        public async Task<CategoryModel> AddCategory(CategoryModel vari)
        {
            var result = await _appDbContext.CategoryModels.AddAsync(vari);

            await _appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<CategoryModel> UpdateCategory(int id, CategoryModel vari)
        {
            var cat = await _appDbContext.CategoryModels.FirstOrDefaultAsync(x => x.Id == id);

            if (cat != null)
            {
                cat.Name = vari.Name;
                cat.IsActive = vari.IsActive;

                await _appDbContext.SaveChangesAsync();

                return cat;
            }
            else
            {
                return cat;
            }
        }

        public async Task DeleteCategory(int ID)
        {
            var result = await _appDbContext.CategoryModels.FirstOrDefaultAsync(x => x.Id == ID);

            if (result != null)
            {
                _appDbContext.Remove(result);

                await _appDbContext.SaveChangesAsync();
            }
        }

        public bool DDeleteCategory(int ID)
        {
            var prod = _appDbContext.CategoryModels.FirstOrDefault(x => x.Id == ID);

            if (prod != null)
            {
                _appDbContext.Remove(prod);

                _appDbContext.SaveChanges();

                return true;
            }


            return false;

        }
    }
}
