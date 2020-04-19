using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required()]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alphabets Only.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alphabets Only.")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Mobile Number")]
        [Required]
        [MinLength(10, ErrorMessage = "Minimum length of 10.")]
        [MaxLength(10, ErrorMessage = "Minimum length of 10.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Numbers Only.")]
        public string MobileNumber { get; set; }

        [Display(Name = "Street Address")]
        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
    }
}