using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceShoppingStore.Entities;
using ECommerceShoppingStore.Exceptions;
using ECommerceShoppingStore.DAL;
using System.Configuration;

/// <summary>
/// AUTHOR:CHIRAG MEHTA
/// DATE:06/10/2018
/// PURPOSE:BL LAYER THE PRODUCTS
/// </summary>

namespace ECommerceShoppingStore.BL
{
    public class ProductBL
    {
        ProductDAL dal = null;

        /// <summary>
        /// METHOD TO VALIDATE THE INPUT DATA BEFORE ADDING IT TO THE DATABASE
        /// </summary>
        /// <param name="product">OBJECT OF CLASS PRODUCT</param>
        /// <returns>TRUE/FALSE</returns>

        public bool Validate(Product product)
        {
            StringBuilder sb = new StringBuilder();
            bool valid = true;
            if (product.ProductID.ToString().Equals(string.Empty))
            {
                valid = false;
                sb.Append(Environment.NewLine + "Please enter the Product ID");
            }
            if (product.ModelName.ToString().Equals(string.Empty))
            {
                valid = false;
                sb.Append(Environment.NewLine + "Please Enter the Model Name");
            }
            if (product.ModelNumber.ToString().Equals(string.Empty))
            {
                valid = false;
                sb.Append(Environment.NewLine + "Please enter the Model Number");
            }
            if (product.UnitCost == 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Please enter the Unit Price");
            }
            if (product.UnitCost.ToString().Any(p => char.IsDigit(p)==false)==true)
            {
                valid= false;
                sb.Append(Environment.NewLine + "Customer Name contains alphabets only.");
            }
            if (product.Description.ToString().Equals(string.Empty))
            {
                valid = false;
                sb.Append(Environment.NewLine + "Please enter a Valid Description");
            }
            return valid;
        }

        /// <summary>
        /// METHOD TO ADD THE CONNECTION STRING
        /// </summary>
        /// <returns></returns>

        public ProductBL(string conString)
        {
            dal = new ProductDAL(ConfigurationManager.ConnectionStrings["EcommStr"].ConnectionString);
        }

        /// <summary>
        /// METHOD TO RETURN ALL VALUES USING SELECT QUERY TO DAL LAYER
        /// </summary>
        /// <returns>LIST OF PRODUCTS </returns>
        
        public List<Product> DisplayAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                products = dal.DisplayAllProducts();
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex) { throw; }
            return products;
        }

        /// <summary>
        /// METHOD TO ADD DATA
        /// </summary>
        /// <returns>VOID</returns>

        public void AddProducts(Product product)
        {
            try
            {
                if (Validate(product))
                    dal.AddProducts(product);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// METHOD TO MODIFY DATA
        /// </summary>
        /// <returns></returns>

        public void UpdateProducts(Product product)
        {
            try
            {
                if (Validate(product))
                    dal.UpdateProducts(product);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// METHOD TO DELETE DATA
        /// </summary>
        /// <returns></returns>
        
        public void DeleteProducts(string name)
        {
            try
            {

                dal.DeleteProducts(name);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        public Product FetchDetails(string id)
        {
            Product products = new Product();
            try
            {
               products= dal.FetchDetails(id);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
            return products;
        }
    }
}

