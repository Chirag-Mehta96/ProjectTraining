using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceShoppingStore.Entities;
using ECommerceShoppingStore.Exceptions;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Windows.Controls;

/// <summary>
/// AUTHOR:CHIRAG MEHTA
/// DATE:06/10/2018
/// PURPOSE:DAL LAYER FOR THE SHOPPING CART
/// </summary>

namespace ECommerceShoppingStore.DAL
{
    public class ShoppingCartDAL
    {
        SqlCommand cmd = null;
        SqlConnection con = null;   //CREATING A REFERENCE FOR THE CLASSSES
        SqlDataReader dr = null;
        SqlDataAdapter ad = new SqlDataAdapter();
        /// <summary>
        /// CONSTRUCTOR INITIALIZING THE SQLCOMMAND OBJECT
        /// </summary>
        /// <param name="conString"></param>

        public ShoppingCartDAL(string conString)
        {
            con = new SqlConnection(conString);
        }

        /// <summary>
        /// METHOD TO DISPLAY ALL THE CART DETAILS FROM THE DATABASE
        /// </summary>
        /// <returns>LIST OF PRODUCTS</returns>


        public DataTable DisplayCartDetails()
        {
            DataTable carts = new DataTable();
            try
            {
                ////USING STORED PROCEDURES
                con.Open();
                cmd = new SqlCommand("[Ecomm].[usp_PopulateCart]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {

                    carts.Load(dr);

                    //ShoppingCart cart = new ShoppingCart();
                    //cart.CartId = Convert.ToInt32(dr[0]);
                    //cart.Quantity = Convert.ToInt32(dr[1]);
                    //cart.ProductId = dr[2].ToString();
                    //cart.DateCreated = Convert.ToDateTime(dr[3]);
                    //carts.Add(cart);

                }
            }
            catch (ECommException) { throw; }
            catch (SqlException) { throw; }
            catch (Exception) { throw; }
            finally
            {
                dr.Close();
                con.Close();
            }
            return carts;

        }

        /// <summary>
        /// METHOD TO ADD THE CART DETAILS TO THE DATABASE
        /// </summary>

        public void AddToCart(ShoppingCart cart)
        {
            try
            {
                //USING STORED PROCEDURES
                cmd = new SqlCommand("Insert into [Ecomm].[ShoppingCart]([Quantity],[Product_Id],[Date_Created]) Values(@quantity,@product_Id,@date_Created)", con);
                cmd.Parameters.AddWithValue("@quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@product_Id", cart.ProductId);
                cmd.Parameters.AddWithValue("@date_Created", cart.DateCreated);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (ECommException) { throw; }
            catch (SqlException) { throw; }
            catch (Exception) { throw; }
            finally { con.Close(); }
        }

        /// <summary>
        /// METHOD TO UPDATE THE CART DETAILS IN THE DATABASE
        /// </summary>

        public void UpdateCart(ShoppingCart cart)
        {
            try
            {
                // USING STORED PROCEDURES

                cmd = new SqlCommand("[Ecomm].[usp_UpdateShoppingCart]", con);
                cmd.Parameters.AddWithValue("@quantity ", cart.Quantity);
                cmd.Parameters.AddWithValue("@product_Id", cart.ProductId);
                cmd.Parameters.AddWithValue("@date_Created", cart.DateCreated);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (ECommException) { throw; }
            catch (SqlException) { throw; }
            catch (Exception) { throw; }
            finally { con.Close(); }
        }

        /// <summary>
        /// METHOD TO DELETE THE CART DETAILS FROM THE DATABASE
        /// </summary>

        public void DeleteCart(ListView id)
        {
            try
            {

                //USING STORED PROCEDURE

                cmd = new SqlCommand("[Ecomm].[usp_DeleteShoppingCart]", con);
                cmd.Parameters.AddWithValue("@Product_Id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();


            }
            catch (ECommException) { throw; }
            catch (Exception) { throw; }
            finally { con.Close(); }
        }

        public int FetchPrice()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("[Ecomm].[usp_Price]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                int price = Convert.ToInt32(cmd.ExecuteScalar());
                return price;
            }
            catch (ECommException) { throw; }
            catch (SqlException) { throw; }
            catch (Exception) { throw; }
            finally { con.Close(); }

        }
        public void ClearCart()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("[Ecomm].[usp_ClearCart]", con);
                cmd.CommandType = CommandType.StoredProcedure;

            }
            catch (ECommException) { throw; }
            catch (SqlException) { throw; }
            catch (Exception) { throw; }
            finally { con.Close(); }
        }

    }
}

