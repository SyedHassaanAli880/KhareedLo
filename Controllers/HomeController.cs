using KhareedLo.Models;
using KhareedLo.Repositories;
using KhareedLo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KhareedLo.Controllers
{
    public class HomeController : Controller
    {
        private readonly GenericRepository<Product> _genR;


        public HomeController(GenericRepository<Product> gr)
        {
            _genR = gr;


        }
        [HttpGet]
        public IActionResult FrontPage()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            var pproducts = _genR.GetAll();

            return View(pproducts);
        }
    }
}
