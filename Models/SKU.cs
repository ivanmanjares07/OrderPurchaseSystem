using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.Models
{
    public class SKU
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
    }
}