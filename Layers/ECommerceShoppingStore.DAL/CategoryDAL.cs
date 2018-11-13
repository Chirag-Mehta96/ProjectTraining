using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceShoppingStore.Entities;
using ECommerceShoppingStore.Exceptions;
using System.Data.SqlClient;


namespace ECommerceShoppingStore.DAL
{
    public class CategoryDAL
    {
        SqlConnection cn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        public CategoryDAL(string conString)
        {
            cn = new SqlConnection(conString);
        }


        /// <summary>
        /// To add Category
        /// </summary>
        /// <param name="cat">New Category</param>
        public void Insert(Category cat)
        {
            try
            {
                //Using Stored Procedure
                cmd = new SqlCommand("[Ecomm].[usp_InsertCategories]", cn);
                cmd.Parameters.AddWithValue("@Category_Id", cat.CategoryId);
                cmd.Parameters.AddWithValue("@Category_Name", cat.CategoryName);
                cmd.Parameters.AddWithValue("@C_Description", cat.Description);
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
        /// To Update Category Details.
        /// </summary>
        /// <param name="cat">Category</param>
        public void Update(Category cat)
        {
            try
            {
                //Using Stored Procedure
                cmd = new SqlCommand("[Ecomm].[usp_UpdateCategories]", cn);
                cmd.Parameters.AddWithValue("@Category_Id", cat.CategoryId);
                cmd.Parameters.AddWithValue("@Category_Name", cat.CategoryName);
                cmd.Parameters.AddWithValue("@C_Description", cat.Description);
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

        public void Delete(string catId)
        {
            try
            {
                //Using Stored Procedure
                cmd = new SqlCommand("[Ecomm].[usp_DeleteCategories]", cn);
                cmd.Parameters.AddWithValue("@Category_Id", catId);       //[parameter from schema , (parameter initialize in delete method)]
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
        //public List<Category> DisplayAllCategories()
        //{
        //    List<Category> categories = new List<Category>();
        //    try
        //    {
        //        //USING STORED PROCEDURES
        //        cn.Open();
        //        cmd = new SqlCommand("[Ecomm].[usp_DisplayCategories]", cn);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
        //        dr = cmd.ExecuteReader();
        //        if (dr.HasRows==true)
        //        {
        //            while (dr.Read())
        //            {
        //                Category cat = new Category();
        //                cat.CategoryId = dr[0].ToString();
        //                cat.CategoryName = dr[1].ToString();
        //                cat.Description = dr[2].ToString();
        //                categories.Add(cat);
        //            }
        //        }
        //    }
        //    catch (ECommException) { throw; }
        //    catch (SqlException) { throw; }
        //    catch (Exception) { throw; }
        //    finally
        //    {
        //        dr.Close();
        //        cn.Close();
        //    }
        //    return categories;

        //}
    }
}
