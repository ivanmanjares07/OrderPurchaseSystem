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
    public class PurchaseItemsRepository
    {
        private readonly string DbConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
        public List<PurchaseItem> GetPurchaseItems()
        {
            var purchaseItems = new List<PurchaseItem>();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(DbConn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllPurchaseItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            foreach (DataRow dr in dt.Rows)
            {
                purchaseItems.Add(
                    new PurchaseItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                        PurchaseOrderId = Convert.ToInt32(dr["PurchaseOrderId"]),
                        SKUId = Convert.ToInt32(dr["SKUId"])
                    });
            }

            return purchaseItems;
        }

        public bool Insert(PurchaseItem purchaseItem)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertPurchaseItemRecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Quantity", purchaseItem.Quantity);
                    cmd.Parameters.AddWithValue("@Price", purchaseItem.Price);
                    cmd.Parameters.AddWithValue("@PurchaseOrderId", purchaseItem.PurchaseOrderId);
                    cmd.Parameters.AddWithValue("@SKUId", purchaseItem.SKUId);

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

        public bool Delete(int id)
        {
            int i = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_DeletePurchaseItemRecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PurchaseItemId", id);

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

        public bool Update(PurchaseItem purchaseItem)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdatePurchaseItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Quantity", purchaseItem.Quantity);
                    cmd.Parameters.AddWithValue("@Price", purchaseItem.Price);
                    cmd.Parameters.AddWithValue("@Id", purchaseItem.Id);

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