using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DynamicMVC.Annotations;

namespace DynamicMVC.UI.Models
{
    [DynamicEntity(ShowDetails = false)]
    [DynamicMenuItem("Customer Regions", "Admin")]
    [DynamicHeader(IndexHeader = "Customer Regions")]
    public class CustomerRegion
    {
        public CustomerRegion()
        {
            Customers = new HashSet<Customer>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; } 
    }
}
