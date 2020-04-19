using OrderPurchaseSystem.Models;
using OrderPurchaseSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.Repository
{
    public class PurchaseOrderRepository
    {
        private readonly string DbConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            var purchaseOrders = new List<PurchaseOrder>();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(DbConn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllPurchaseOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            foreach (DataRow dr in dt.Rows)
            {
                purchaseOrders.Add(
                    new PurchaseOrder
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        DateOfDelivery = Convert.ToDateTime(dr["DateOfDelivery"]),
                        Status = dr["Status"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        AmountDue = Convert.ToDecimal(dr["AmountDue"]),
                        CustomerId = Convert.ToInt32(dr["CustomerId"])
                    });
            }

            return purchaseOrders;
        }

        public bool Insert(OrderTakingForm purchaseOrder)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertPurchaseOrderRecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DateOfDelivery", Convert.ToDateTime(purchaseOrder.PurchaseOrder.DateOfDelivery));
                    cmd.Parameters.AddWithValue("@Status", purchaseOrder.PurchaseOrder.Status);
                    cmd.Parameters.AddWithValue("@DateCreated", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    cmd.Parameters.AddWithValue("@IsActive", "1");
                    cmd.Parameters.AddWithValue("@CustomerId", purchaseOrder.PurchaseOrder.CustomerId);

                    i = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool Update(OrderTakingEditForm purchaseOrder)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdatePurchaseOrderRecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DateOfDelivery", Convert.ToDateTime(purchaseOrder.PurchaseOrder.DateOfDelivery));
                    cmd.Parameters.AddWithValue("@Status", purchaseOrder.PurchaseOrder.Status);
                    cmd.Parameters.AddWithValue("@CustomerId", purchaseOrder.PurchaseOrder.CustomerId);
                    cmd.Parameters.AddWithValue("@AmountDue", purchaseOrder.PurchaseOrder.AmountDue);
                    cmd.Parameters.AddWithValue("@PurchaseOrderId", purchaseOrder.PurchaseOrder.Id);

                    i = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}