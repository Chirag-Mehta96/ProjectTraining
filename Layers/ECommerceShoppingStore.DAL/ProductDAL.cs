using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceShoppingStore.Entities;
using ECommerceShoppingStore.Exceptions;
using System.Data.SqlClient;

/// <summary>
/// AUTHOR:CHIRAG MEHTA
/// DATE:06/10/2018
/// PURPOSE:DAL LAYER THE PRODUCTS
/// </summary>


namespace ECommerceShoppingStore.DAL
{
    public class ProductDAL
    {
        SqlCommand cmd = null;        
        SqlConnection con = null;   //CREATING A REFERENCE FOR THE CLASSSES
        SqlDataReader dr = null;

        /// <summary>
        /// CONSTRUCTOR INITIALIZING THE SQLCOMMAND OBJECT
        /// </summary>
        /// <param name="conString"></param>

        public ProductDAL(string conString)
        {
            con = new SqlConnection(conString);
        }

        /// <summary>
        /// METHOD TO DISPLAY ALL THE PRODUCT DETAILS FROM THE DATABASE
        /// </summary>
        /// <returns>LIST OF PRODUCTS</returns>

        public List<Product> DisplayAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                //USING STORED PROCEDURES
                cmd = new SqlCommand("[Ecomm].[usp_SelectAllProductDetails]",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Product pro = new Product();
                        pro.ProductID = dr[0].ToString();
                        pro.CategoryID = dr[1].ToString();
                        pro.ModelNumber = dr[2].ToString();
                        pro.ModelName = dr[3].ToString();
                        pro.UnitCost = Convert.ToInt32(dr[4]);
                        pro.Description = dr[5].ToString();
                        products.Add(pro);
                    }
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
            return products;
            
        }

        /// <summary>
        /// METHOD TO ADD THE PRODUCT DETAILS TO THE DATABASE
        /// </summary>

        public void AddProducts(Product product)
        {
            try
            {
                //USING STORED PROCEDURES
                cmd = new SqlCommand("[Ecomm].[usp_InsertProducts]", con);
                cmd.Parameters.AddWithValue("@Product_id", product.ProductID);
                cmd.Parameters.AddWithValue("@category_id", product.CategoryID);
                cmd.Parameters.AddWithValue("@modelnum ", product.ModelNumber);
                cmd.Parameters.AddWithValue("@modelname", product.ModelName);
                cmd.Parameters.AddWithValue("@unitcost", product.UnitCost);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (ECommException) { throw; }
            catch (SqlException) { throw; }
            catch(Exception) { throw; }
            finally { con.Close();}
        }

        /// <summary>
        /// METHOD TO UPDATE THE PRODUCT DETAILS IN THE DATABASE
        /// </summary>
        
        public void UpdateProducts(Product product)
        {
            try
            {
                // USING STORED PROCEDURES

                cmd = new SqlCommand("[Ecomm].[usp_UpdateProduct]", con);
                cmd.Parameters.AddWithValue("@Product_id", product.ProductID);
                cmd.Parameters.AddWithValue("@category_id", product.CategoryID);
                cmd.Parameters.AddWithValue("@modelnum ", product.ModelNumber);
                cmd.Parameters.AddWithValue("@modelname", product.ModelName);
                cmd.Parameters.AddWithValue("@unitcost", product.UnitCost);
                cmd.Parameters.AddWithValue("@description", product.Description);
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
        /// METHOD TO DELETE THE PRODUCT DETAILS FROM THE DATABASE
        /// </summary>

        public void DeleteProducts(string name)
        {
            try
            {

                //USING STORED PROCEDURE

                cmd = new SqlCommand("[Ecomm].[usp_DeleteProductsAdmin]", con);
                cmd.Parameters.AddWithValue("@model_name",name);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();


            }
            catch (ECommException) { throw; }
            catch (Exception ) { throw; }
            finally { con.Close(); }
        }
        public Product FetchDetails(string id)
        {
            Product products = new Product();
            try
            {
                //Using Stored Procedure
                cmd = new SqlCommand("[Ecomm].[usp_FetchDetails]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ProductId", id);
                con.Open();
                dr = cmd.ExecuteReader();       //ExecuteReader used for reading all data
                if (dr.HasRows)                 //Return true or false if row is there.
                {
                    //dr.Read() will move index to +1 position(True) and read data Rowwise.
                    while (dr.Read())
                    {
                       
                        products.ModelName = dr["Model_Name"].ToString();
                        products.UnitCost = Convert.ToInt32(dr["Unit_Cost"]);
                        products.Description=dr["P_Description"].ToString();
                        
                    }
                }
            }
            catch (ECommException Ex) { throw; }
            catch (Exception ex2) { throw; }
            finally
            {
                dr.Close();
                con.Close();
            }
            return products;
        }
    }

}


