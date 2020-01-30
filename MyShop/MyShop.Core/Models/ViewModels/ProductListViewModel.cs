using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
