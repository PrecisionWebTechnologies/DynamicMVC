using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DynamicMVC.Annotations;

namespace DynamicMVC.UI.Models
{
    [DynamicEntity(EditProperties = "OrderId,ProductId,Quantity", CreateProperties = "OrderId,ProductId,Quantity", InstanceIncludes = "Product")]
    [DynamicMenuItemExclude]
    public class OrderLine
    {
        public int Id { get; set; }
        [FilterUIHint("None")]
        [UIHint("DynamicEditorHidden")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [DynamicSortNone]
        public decimal Total
        {
            get
            {
                if (Product == null)
                    return 0;
                return Product.Price * Quantity;
            }
        }

    }
}