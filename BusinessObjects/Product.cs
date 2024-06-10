﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public short? UnitsInStock { get; set; }
        public decimal? UnitPrice { get; set; }
        public virtual Category Category { get; set; }

        public Product(int productId, string productName, int? categoryId,
            short? unitsInStock, decimal? unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            CategoryId = categoryId;
            UnitsInStock = unitsInStock;
            UnitPrice = unitPrice;
        }
        public Product() { }
    }
}
