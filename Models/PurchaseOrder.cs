using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        
        [Display(Name = "Date of Delivery")]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        [MinTomorrowDateOfDelivery]
        public DateTime DateOfDelivery { get; set; }

        [Required]
        public string Status { get; set; }

        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Full Name")]
        public int CustomerId { get; set; }
    }
}