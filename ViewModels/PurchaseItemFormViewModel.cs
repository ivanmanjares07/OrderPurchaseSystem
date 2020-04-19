using OrderPurchaseSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.ViewModels
{
    public class PurchaseItemFormViewModel
    {
        public List<SKU> SKUs { get; set; }
        public PurchaseItem PurchaseItem { get; set; }
    }
}