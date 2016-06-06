using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DynamicMVC.Annotations;

namespace DynamicMVC.UI.Models
{
    [DynamicEntity]
    public class OrderType
    {
        public OrderType()
        {
            Orders = new List<Order>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}