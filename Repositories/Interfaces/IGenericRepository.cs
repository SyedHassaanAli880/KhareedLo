using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        TEntity GetById(object obj);

        void Insert(TEntity obj);

        TEntity Update(TEntity obj);

        bool Delete(object obj);
    }
}
