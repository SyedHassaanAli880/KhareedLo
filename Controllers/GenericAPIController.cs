using KhareedLo.Models;
using KhareedLo.Repositories;
using KhareedLo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KhareedLo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericAPIController<T> : ControllerBase where T : class
    {
        private readonly AppDbContext _appdbcontext;

        private readonly IGenericRepository<T> _gr;

        public GenericAPIController(AppDbContext apdt, IGenericRepository<T> genR)
        {
            _appdbcontext = apdt;

            _gr = genR;
        }


        //[HttpPost("add-T")]
        //public IActionResult Add(T entity)
        //{
        //    var _prod = new T()
        //    {
        //        Name = entity.Name,
        //        Price = entity.Price,
        //        //Category = entity.Category,
        //        ImagePhoto = entity.ImagePhoto,
        //        IsInStock = entity.IsInStock,
        //        LongDescription = entity.LongDescription,
        //        Quantity = entity.Quantity,
        //        ShortDescription = entity.ShortDescription
        //    };

        //    _TRepository.AddT(_prod);
        //    return Ok();
        //}


        [HttpPost("create-object")]
        public IActionResult Create(T entity)
        {
            _gr.Insert(entity);

            return Ok();
        }

        [HttpGet("get-all-entityObjects")]
        public IActionResult GetAllObjects()
        {
            List<T> entity = _gr.GetAll();

            return Ok(entity);
        }

        [HttpGet("get-entity-by-id/{id}")]
        public IActionResult GetObjectById(int id)
        {
            var obj = _gr.GetById(id);
            
            if(obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPut("update-entity")]
        public IActionResult UpdateById(T obj)
        {
            var update = _gr.Update(obj);

            return Ok(update);
        }

        [HttpDelete("Delete-entityObject")]
        public IActionResult DeleteById(int id)
        {
            _gr.Delete(id);

            return Ok();
        }
    }
}
