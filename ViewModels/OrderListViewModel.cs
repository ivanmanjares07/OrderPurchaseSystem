using OrderPurchaseSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.ViewModels
{
    public class OrderListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }

        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }

    }
}