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
    public class OrderStatusRepository
    {
        private readonly string DbConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString();

        public List<OrderStatus> GetOrderStatus()
        {
            var statuses = new List<OrderStatus>();
            DataTable dt = new DataTable();

            using(SqlConnection con = new SqlConnection(DbConn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllOrderStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            foreach (DataRow dr in dt.Rows)
            {
                statuses.Add(
                    new OrderStatus
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Status = dr["Status"].ToString()
                    });
            }

            return statuses;
        }
    }
}