using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.Models
{
    public class PurchaseItem
    {
        public int Id { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The Quantity field must be at least one.")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }


        public int PurchaseOrderId { get; set; }

        [Display(Name = "SKU")]
        public int SKUId { get; set; }
    }
}