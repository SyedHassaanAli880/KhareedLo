using KhareedLo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using KhareedLo.Models.Category;
using System.Threading.Tasks;

namespace KhareedLo.ViewModel
{
    public class CheckOutViewModel
    {
        public List<KhareedLo.Models.Products> CartProducts { get; set; }

        public List<int> CartProductIDs { get; set; }
    }
}
