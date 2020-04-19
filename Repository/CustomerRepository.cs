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
    public class CustomerRepository
    {
        private readonly string DbConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString();

        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(DbConn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllCustomers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            foreach (DataRow dr in dt.Rows)
            {
                customers.Add(
                    new Customer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        FullName = dr["FullName"].ToString(),
                        MobileNumber = dr["MobileNumber"].ToString(),
                        StreetAddress = dr["StreetAddress"].ToString(),
                        City = dr["City"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    });
            }

            return customers;
        }

        public bool Insert(Customer customer)
        {

            customer.FullName = customer.FirstName + " " + customer.LastName;
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertCustomerRecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@FullName", customer.FullName);
                    cmd.Parameters.AddWithValue("@MobileNumber", customer.MobileNumber);
                    cmd.Parameters.AddWithValue("@StreetAddress", customer.StreetAddress);
                    cmd.Parameters.AddWithValue("@City", customer.City);
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

        public bool Update(Customer customer)
        {

            customer.FullName = customer.FirstName + " " + customer.LastName;
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DbConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateCustomerRecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", customer.Id);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@FullName", customer.FullName);
                    cmd.Parameters.AddWithValue("@MobileNumber", customer.MobileNumber);
                    cmd.Parameters.AddWithValue("@StreetAddress", customer.StreetAddress);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(customer.IsActive));

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
                    SqlCommand cmd = new SqlCommand("sp_DeleteCustomerRecord", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerId", id);

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