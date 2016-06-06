using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using DynamicMVC.Annotations;

namespace DynamicMVC.UI.Models
{
    [DynamicEntity(CreateProperties = "CustomerId", ListIncludes = "Customer, OrderLines.Product, OrderStatus"
        , InstanceIncludes = "Customer, OrderLines.Product, OrderStatus", IndexProperties = "CustomerId ,Total,OrderStatusId")]
    [DynamicMenuItem("Order", "Order Entry")]
    public class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
            OrderDate = DateTime.Now;
            OrderStatusId = 1;
        }
        public int Id { get; set; }
        [DynamicFilterDateRange("Start Date", "End Date", "StartDate", "EndDate", -100, Order = -1)]
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } 
        [DataType(DataType.Currency)]
        [DynamicSortNone]
// ReSharper disable once Mvc.TemplateNotResolved
        [UIHint("DynamicEditorReadOnly")]
        public decimal Total
        {
            get { return OrderLines.Sum(x => x.Total); }
        }
        [DisplayName("Order Status")]
        [DynamicFilterDropDown("Order Status", "Select Status")]
        public int OrderStatusId { get; set; }
        [DisplayName("Order Status")]
        public OrderStatus OrderStatus { get; set; }

        [DynamicFilterBoolean("Is Hot Order", "is", "test")]
        public bool IsHotOrder { get; set; }
        public bool? IsReallyHotOrder { get; set; }
        public int? OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }

        public void MakeHot()
        {
            IsHotOrder = true;
        }

        public DateTime? RequestedDateTime { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
       
    }
}