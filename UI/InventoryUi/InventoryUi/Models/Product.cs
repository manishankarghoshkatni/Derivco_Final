using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryUi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public decimal Price { get; set; }
        public string Currency { get; set; }

        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitDescription { get; set; }
    }
}