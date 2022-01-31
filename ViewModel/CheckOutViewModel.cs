using System.Collections.Generic;

namespace KhareedLo.ViewModel
{
    public class CheckOutViewModel
    {
        public List<Models.Product> CartProducts { get; set; }

        public List<int> CartProductIDs { get; set; }
    }
}
