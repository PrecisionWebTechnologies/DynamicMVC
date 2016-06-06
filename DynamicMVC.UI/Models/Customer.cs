using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DynamicMVC.Annotations;

namespace DynamicMVC.UI.Models
{
    [DynamicEntity(ShowDelete = false)]
    [DynamicMenuItem("Customer", "Order Entry")]
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        public int Id { get; set; }
        [Required]
        [DynamicFilterAutoComplete("Customer Name", "CustomerName", typeof(Customer), "Name", "Name")]
        public string Name { get; set; }
        [DisplayName("Region")]
        public int CustomerRegionId { get; set; }
        public CustomerRegion CustomerRegion { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}