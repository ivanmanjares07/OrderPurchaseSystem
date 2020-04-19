using OrderPurchaseSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.ViewModels
{
    public class OrderTakingForm
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }

        public List<OrderStatus> OrderStatuses { get; set; }
        public List<Customer> Customers { get; set; }
    }
}