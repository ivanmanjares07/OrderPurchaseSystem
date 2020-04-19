using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.Models
{
    public class MinTomorrowDateOfDelivery : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var purchaseOrder = (PurchaseOrder)validationContext.ObjectInstance;

            if(purchaseOrder.DateOfDelivery == null)
            {
                return new ValidationResult("The Date of Delivery field is required.");
            }

            if (purchaseOrder.DateOfDelivery >= DateTime.Now)
                return ValidationResult.Success;
            else
                return new ValidationResult("Date of Delivery cannot be today or previous days.");
        }
    }
}