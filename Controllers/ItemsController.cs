using OrderPurchaseSystem.Models;
using OrderPurchaseSystem.Repository;
using OrderPurchaseSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderPurchaseSystem.Controllers
{
    public class ItemsController : Controller
    {

        // GET: Items/Create
        public ActionResult Create()
        {
            SKURepository skuRepo = new SKURepository();

            var viewModel = new PurchaseItemFormViewModel()
            {
                SKUs = skuRepo.GetSKUs(),
            };

            return View(viewModel);
        }

        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(PurchaseItem purchaseItem)
        {
            try
            {
                SKURepository skuRepo = new SKURepository();

                if (!ModelState.IsValid)
                {
                    var viewModel = new PurchaseItemFormViewModel()
                    {
                        SKUs = skuRepo.GetSKUs(),
                        PurchaseItem = purchaseItem
                    };

                    return View(viewModel);
                }

                PurchaseItemsRepository purchaseItemsRepo = new PurchaseItemsRepository();
                
                var sku = skuRepo.GetSKUs().Find(x => x.Id == purchaseItem.SKUId);
                purchaseItem.Price = sku.UnitPrice * purchaseItem.Quantity;
                purchaseItem.PurchaseOrderId = Convert.ToInt32(Session["PurchaseOrderId"]);

                purchaseItemsRepo.Insert(purchaseItem);

                Session.Clear();

                return RedirectToAction("Edit", "Order", new { @id = purchaseItem.PurchaseOrderId});
            }
            catch
            {
                return View();
            }
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            SKURepository skuRepo = new SKURepository();
            PurchaseItemsRepository purchaseItemsRepo = new PurchaseItemsRepository();

            var viewModel = new PurchaseItemFormViewModel()
            {
                SKUs = skuRepo.GetSKUs(),
                PurchaseItem = purchaseItemsRepo.GetPurchaseItems().Find(x => x.Id == id)
            };

            return View(viewModel);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(PurchaseItem purchaseItem)
        {
            try
            {
                SKURepository skuRepo = new SKURepository();

                if (!ModelState.IsValid)
                {
                    var viewModel = new PurchaseItemFormViewModel()
                    {
                        SKUs = skuRepo.GetSKUs(),
                        PurchaseItem = purchaseItem
                    };

                    return View(viewModel);
                }

                PurchaseItemsRepository purchaseItemsRepo = new PurchaseItemsRepository();

                var sku = skuRepo.GetSKUs().Find(x => x.Id == purchaseItem.SKUId);
                purchaseItem.Price = sku.UnitPrice * purchaseItem.Quantity;
                purchaseItem.PurchaseOrderId = Convert.ToInt32(Session["PurchaseOrderId"]);

                decimal amountDue = purchaseItemsRepo.GetPurchaseItems().Where(x => x.PurchaseOrderId == purchaseItem.PurchaseOrderId).Sum(x => x.Price);


                purchaseItemsRepo.Update(purchaseItem);

                Session.Clear();

                return RedirectToAction("Edit", "Order", new { @id = purchaseItem.PurchaseOrderId });

            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                PurchaseItemsRepository purchaseItemRepo = new PurchaseItemsRepository();
                purchaseItemRepo.Delete(id);

                int purchaseOrderId = Convert.ToInt32(Session["PurchaseOrderId"]);

                Session.Clear();

                return RedirectToAction("Edit", "Order", new { @id = purchaseOrderId });
            }
            catch
            {
                return View();
            }
        }
    }
}
