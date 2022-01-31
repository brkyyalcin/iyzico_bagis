using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iyzico_freelancer.data
{
    public class Product
    {
        public string StockCode { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public Decimal Price { get; set; }
    }
}
