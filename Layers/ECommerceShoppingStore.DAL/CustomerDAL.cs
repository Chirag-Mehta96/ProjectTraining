using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceShoppingStore.Entities;
using ECommerceShoppingStore.Exceptions;
using System.Data.SqlClient;

/// <summary>
/// AUTHOR:DIPLAI MALI
/// DATE:06/10/2018
/// PURPOSE:DAL LAYER THE PRODUCTS
/// </summary>


namespace ECommerceShoppingStore.DAL
{
     public class CustomerDAL
    {
        SqlConnection cn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;

        /// <summary>
        /// CONSTRUCTOR INITIALIZING THE SQLCOMMAND OBJECT
        /// </summary>
        /// <param name="conString"></param>

        public CustomerDAL(string conString)
        {
            cn = new SqlConnection(conString);
        }

        /// <summary>
        /// METHOD TO DISPLAY ALL THE CUSTOMER DETAILS FROM THE DATABASE
        /// </summary>
        /// <returns>LIST OF CUSTOMERS</returns>

        public List<Customer> SelectAll()
        {
            List<Customer> custs = new List<Customer>();
            try
            {
                //Using Stored Procedure
                cmd = new SqlCommand("[Ecomm].[usp_SelectAllProducts]", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                dr = cmd.ExecuteReader();       //ExecuteReader used for reading all data
                if (dr.HasRows)                 //Return true or false if row is there.
                {
                    //dr.Read() will move index to +1 position(True) and read data Rowwise.
                    while (dr.Read())
                    {
                        Customer c = new Customer();
                        c.CustomerId = Convert.ToInt32(dr[0]);
                        c.CustomerFullName = dr[1].ToString();
                        c.EmailId = dr[2].ToString();
                        c.Password = dr[3].ToString();
                        c.DeliveryAddress = dr[4].ToString();
                        custs.Add(c);
                    }
                }
            }
            catch (ECommException Ex) { throw; }
            catch (Exception ex2) { throw; }
            finally
            {
                dr.Close();
                cn.Close();
            }
            return custs;
        }

        /// <summary>
        /// METHOD TO ADD THE CUSTOMER DETAILS TO THE DATABASE
        /// </summary>

        public void Insert(Customer cust)
        {
            try
            {
                //Using Stored Procedure
                cmd = new SqlCommand("[Ecomm].[usp_InsertCustomer]", cn);
               // cmd.Parameters.AddWithValue("@Customer_Id", cust.CustomerId);          //[parameter from schema , (methods parameter name).(propertie's name)]
                cmd.Parameters.AddWithValue("@Full_Name", cust.CustomerFullName);
                cmd.Parameters.AddWithValue("@Email", cust.EmailId);
                cmd.Parameters.AddWithValue("@Password", cust.Password);
                cmd.Parameters.AddWithValue("@Delivery_Address", cust.DeliveryAddress);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (ECommException ex1)
            { throw; }
            catch (Exception Ex)
            { throw; }
            finally { }
        }

        /// <summary>
        /// METHOD TO UPDATE THE CUSTOMER DETAILS IN THE DATABASE
        /// </summary>

        public void Update(Customer cust)
        {
            try
            {
                //Using Stored Procedure
                cmd = new SqlCommand("[Ecomm].[usp_UpdateCustomer]", cn);
                cmd.Parameters.AddWithValue("@Customer_Id", cust.CustomerId);          //[parameter from schema , (methods parameter name).(propertie's name)]
                cmd.Parameters.AddWithValue("@Full_Name", cust.CustomerFullName);
                cmd.Parameters.AddWithValue("@Email", cust.EmailId);
                cmd.Parameters.AddWithValue("@Password", cust.Password);
                cmd.Parameters.AddWithValue("@Delivery_Address", cust.DeliveryAddress);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (ECommException ex1)
            { throw; }
            catch (Exception Ex)
            { throw; }
            finally { cn.Close(); }
        }

        /// <summary>
        /// METHOD TO DELETE THE CUSTOMER DETAILS FROM THE DATABASE
        /// </summary>

        public void Delete(int custId)
        {
            try
            {
                //Using Stored Procedure
                cmd = new SqlCommand("[Ecomm].[usp_DeleteCustomer]", cn);
                cmd.Parameters.AddWithValue("@Customer_Id", custId);       //[parameter from schema , (parameter initialize in delete method)]
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (ECommException ex1)
            { throw; }
            catch (Exception Ex)
            { throw; }
            finally { }
        }
       
    }
}
