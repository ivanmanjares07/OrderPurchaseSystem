using OrderPurchaseSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderPurchaseSystem.Repository
{
    public class SKURepository
    {
        private readonly string DbConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString();

        public List<SKU> GetSKUs()
        {
            var skus = new List<SKU>();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(DbConn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetSkus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            foreach (DataRow dr in dt.Rows)
            {
                skus.Add(
                    new SKU
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Code = dr["Code"].ToString(),
                        UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    });
                    
            }

            return skus;
        }

        public bool Insert(SKU sku)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertSKURecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", sku.Name);
                    cmd.Parameters.AddWithValue("@Code", sku.Code);
                    cmd.Parameters.AddWithValue("@UnitPrice", sku.UnitPrice);
                    cmd.Parameters.AddWithValue("@DateCreated", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    cmd.Parameters.AddWithValue("@IsActive", "1");

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

        public bool Update(SKU sku)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateSKURecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SKUId", sku.Id);
                    cmd.Parameters.AddWithValue("@Name", sku.Name);
                    cmd.Parameters.AddWithValue("@Code", sku.Code);
                    cmd.Parameters.AddWithValue("@UnitPrice", sku.UnitPrice);
                    cmd.Parameters.AddWithValue("@IsActive", "1");

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
                    SqlCommand cmd = new SqlCommand("sp_DeleteSKURecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SKUId", id);

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