using System.Collections.Generic;
using System.Linq;
using KhareedLo.Models;
using KhareedLo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KhareedLo.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal AppDbContext context;

        internal DbSet<TEntity> dbset;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;

            this.dbset = context.Set<TEntity>();
        }

        public bool Delete(object id)
        {
            TEntity entityToDelete = dbset.Find(id);

            if (entityToDelete != null)
            {
                return Delete(entityToDelete);
            }
            else
            {
                return false;
            }

        }

        public bool Delete(TEntity entityToDelete)
        {
            if(context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbset.Attach(entityToDelete);
                context.SaveChanges();
                return false;
            }
            else
            {
                dbset.Remove(entityToDelete);
                context.SaveChanges();
                return true;
            }
            
        }

        public List<TEntity> GetAll()
        {
            return dbset.ToList();
        }

        public TEntity GetById(object id)
        {
            return dbset.Find(id);
        }

        public void Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);

            context.SaveChanges();
        }

        public TEntity Update(TEntity entityToUpdate)
        {
            dbset.Attach(entityToUpdate);

            context.Entry(entityToUpdate).State = EntityState.Modified;

            context.SaveChanges();

            return entityToUpdate;
        }

        public List<Product> GetAllProductsById(List<int> IDs)
        {
            return context.Products.Where(p => IDs.Contains(p.Id)).ToList();
        }
    }
}
