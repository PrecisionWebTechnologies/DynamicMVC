using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DynamicMVC.Annotations;

namespace DynamicMVC.UI.Models
{
    [DynamicEntity(ShowDetails = false)]
    [DynamicHeader(IndexHeader = "Products")]
    [DynamicMenuItem("Product", "Order Entry")]
    public class Product
    {
        public Product()
        {
            CreateDate=DateTime.Now;
            OrderLines = new HashSet<OrderLine>();
        }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; } 
    }
}