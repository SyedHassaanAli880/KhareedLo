using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo.Controllers.Computer_Components
{
    public class ComputerComponentsController : Controller
    {
        public IActionResult CCDisplayPage()
        {
            return View();
        }
    }
}
