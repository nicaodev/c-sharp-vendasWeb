using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWeb.Models;

namespace VendasWeb.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
