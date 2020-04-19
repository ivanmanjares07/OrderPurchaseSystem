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
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var list = new List<OrderListViewModel>();

            CustomerRepository custRepo = new CustomerRepository();
            PurchaseOrderRepository orderRepo = new PurchaseOrderRepository();

            var orders = orderRepo.GetPurchaseOrders();
            var customers = custRepo.GetCustomers();

            var orderList = from order in orders
                            join customer in customers on order.CustomerId equals customer.Id
                            select new { 
                                CustomerFullName = customer.FullName, 
                                DeliveryDate = order.DateOfDelivery,
                                Status = order.Status,
                                AmountDue = order.AmountDue,
                                Id = order.Id
                            };

            foreach (var item in orderList)
            {
                list.Add(
                    new OrderListViewModel
                    {
                        FullName = item.CustomerFullName,
                        DeliveryDate = item.DeliveryDate,
                        Status = item.Status,
                        AmountDue = item.AmountDue,
                        Id = item.Id
                    });
            }

            return View(list);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            OrderStatusRepository orderStatusRepo = new OrderStatusRepository();
            CustomerRepository custRepo = new CustomerRepository();
            PurchaseItemsRepository purchaseItemRepo = new PurchaseItemsRepository();

            var order = new OrderTakingForm()
            {
                OrderStatuses = orderStatusRepo.GetOrderStatus(),
                PurchaseItems = purchaseItemRepo.GetPurchaseItems(),
                Customers = custRepo.GetCustomers(),
            };

            return View(order);
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(OrderTakingForm order)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    OrderStatusRepository orderStatusRepo = new OrderStatusRepository();
                    CustomerRepository custRepo = new CustomerRepository();

                    var viewModel = new OrderTakingForm()
                    {
                        Customers = custRepo.GetCustomers(),
                        OrderStatuses = orderStatusRepo.GetOrderStatus(),
                        PurchaseOrder = order.PurchaseOrder
                    };

                    return View(viewModel);
                }

                PurchaseOrderRepository purchaseItemsRepo = new PurchaseOrderRepository();
                purchaseItemsRepo.Insert(order);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            OrderStatusRepository orderStatusRepo = new OrderStatusRepository();
            CustomerRepository custRepo = new CustomerRepository();
            PurchaseOrderRepository purchaseOrder = new PurchaseOrderRepository();
            PurchaseItemsRepository purchaseItemsRepo = new PurchaseItemsRepository();
            SKURepository skuRepo = new SKURepository();

            var list = new List<ItemListViewModel>();

            var order = purchaseOrder.GetPurchaseOrders().Find(m => m.Id == id);
            var purchaseItems = purchaseItemsRepo.GetPurchaseItems().Where(x => x.PurchaseOrderId == order.Id).ToList();
            var skus = skuRepo.GetSKUs();

            var itemList =  from purchaseItem in purchaseItems
                            join sku in skus on purchaseItem.SKUId equals sku.Id
                            select new
                            {
                                SKU = sku.Name,
                                Quantity = purchaseItem.Quantity,
                                Price = purchaseItem.Price,
                                PurchaseItemId = purchaseItem.Id
                            };

            foreach (var item in itemList)
            {
                list.Add(
                    new ItemListViewModel
                    {
                        SKU = item.SKU,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        PurchaseItemId = item.PurchaseItemId
                    });
            }
            order.AmountDue = purchaseItemsRepo.GetPurchaseItems().Where(x => x.PurchaseOrderId == order.Id).Sum(x => x.Price);
            var viewModel = new OrderTakingEditForm()
            {
                PurchaseOrder = order,
                OrderStatuses = orderStatusRepo.GetOrderStatus(),
                Customers = custRepo.GetCustomers(),
                Items = list
            };

            Session["PurchaseOrderId"] = order.Id;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(OrderTakingEditForm order)
        {
            try
            {
                OrderStatusRepository orderStatusRepo = new OrderStatusRepository();
                CustomerRepository custRepo = new CustomerRepository();
                PurchaseItemsRepository purchaseItemsRepo = new PurchaseItemsRepository();
                PurchaseOrderRepository purchaseOrderRepo = new PurchaseOrderRepository();
                SKURepository skuRepo = new SKURepository();

                if (!ModelState.IsValid)
                {
                    var items = purchaseItemsRepo.GetPurchaseItems().Where(x => x.PurchaseOrderId == order.PurchaseOrder.Id).ToList();
                    var skus = skuRepo.GetSKUs();
                    var list = new List<ItemListViewModel>();

                    var itemList = from purchaseItem in items
                                   join sku in skus on purchaseItem.SKUId equals sku.Id
                                   select new
                                   {
                                       SKU = sku.Name,
                                       Quantity = purchaseItem.Quantity,
                                       Price = purchaseItem.Price,
                                       PurchaseItemId = purchaseItem.Id
                                   };

                    foreach (var item in itemList)
                    {
                        list.Add(
                            new ItemListViewModel
                            {
                                SKU = item.SKU,
                                Quantity = item.Quantity,
                                Price = item.Price,
                                PurchaseItemId = item.PurchaseItemId
                            });
                    }

                    var viewModel = new OrderTakingEditForm()
                    {
                        Customers = custRepo.GetCustomers(),
                        OrderStatuses = orderStatusRepo.GetOrderStatus(),
                        PurchaseOrder = order.PurchaseOrder,
                        Items = list
                    };

                    return View(viewModel);
                }

                order.PurchaseOrder.AmountDue = 0;

                var purchaseItems = purchaseItemsRepo.GetPurchaseItems().Where(x => x.PurchaseOrderId == order.PurchaseOrder.Id); ;

                foreach (var item in purchaseItems)
                {
                    order.PurchaseOrder.AmountDue += item.Price;
                }

                purchaseOrderRepo.Update(order);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
