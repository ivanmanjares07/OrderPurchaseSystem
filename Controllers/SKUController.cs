using OrderPurchaseSystem.Models;
using OrderPurchaseSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderPurchaseSystem.Controllers
{
    public class SKUController : Controller
    {
        // GET: SKU
        public ActionResult Index()
        {
            SKURepository skuRepo = new SKURepository();
            var skus = skuRepo.GetSKUs();

            return View(skus);
        }

        // GET: SKU/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SKU/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SKU/Create
        [HttpPost]
        public ActionResult Create(SKU sku)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(sku);
                }

                SKURepository skuRepo = new SKURepository();
                skuRepo.Insert(sku);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SKU/Edit/5
        public ActionResult Edit(int id)
        {
            SKURepository skuRepo = new SKURepository();
            var sku = skuRepo.GetSKUs().Find(m => m.Id == id);

            return View(sku);
        }

        // POST: SKU/Edit/5
        [HttpPost]
        public ActionResult Edit(SKU sku)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(sku);
                }

                SKURepository skuRepo = new SKURepository();
                skuRepo.Update(sku);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: SKU/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                SKURepository skuRepo = new SKURepository();
                skuRepo.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
