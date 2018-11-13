using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceShoppingStore.Entities;
using ECommerceShoppingStore.Exceptions;
using ECommerceShoppingStore.DAL;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Windows.Controls;

/// <summary>
/// AUTHOR:CHIRAG MEHTA
/// DATE:06/10/2018
/// PURPOSE:BL LAYER THE PRODUCTS
/// </summary>

namespace ECommerceShoppingStore.BL
{
    public class ShoppingCartBL
    {
        ShoppingCartDAL cartdal = null;

        /// <summary>
        /// METHOD TO VALIDATE THE INPUT DATA BEFORE ADDING IT TO THE DATABASE
        /// </summary>
        /// <param name="product">OBJECT OF CLASS PRODUCT</param>
        /// <returns>TRUE/FALSE</returns>

        public bool Validate(ShoppingCart cart)
        {
            StringBuilder sb = new StringBuilder();
            bool valid = true;
            if (cart.Quantity == 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Please enter the Quantity");
            }
            if (cart.ProductId.Equals(string.Empty))
            {
                valid = false;
                sb.Append(Environment.NewLine + "Please enter the Product Id");
            }
            if (cart.DateCreated.Equals(DateTime.MinValue))
            {
                valid = false;
                sb.Append(Environment.NewLine + "Please enter a Valid date");
            }
            return valid;
        }

        /// <summary>
        /// METHOD TO ADD THE CONNECTION STRING
        /// </summary>
        /// <returns></returns>

        public ShoppingCartBL(string conString)
        {
            cartdal = new ShoppingCartDAL(ConfigurationManager.ConnectionStrings["EcommStr"].ConnectionString);
        }

        /// <summary>
        /// METHOD TO RETURN ALL VALUES USING SELECT QUERY TO DAL LAYER
        /// </summary>
        /// <returns>LIST OF PRODUCTS </returns>

        public DataTable DisplayCartDetails()
        {
            DataTable carts = new DataTable();
            try
            {
                carts = cartdal.DisplayCartDetails();
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex) { throw; }
            return carts;
        }

        /// <summary>
        /// METHOD TO ADD DATA
        /// </summary>
        /// <returns>VOID</returns>

        public void AddToCart(ShoppingCart cart)
        {
            try
            {
                if(Validate(cart))
                    cartdal.AddToCart(cart);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// METHOD TO MODIFY DATA
        /// </summary>
        /// <returns></returns>

        public void UpdateCart(ShoppingCart cart)
        {
            try
            {
                if (Validate(cart))
                    cartdal.UpdateCart(cart);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// METHOD TO DELETE DATA
        /// </summary>
        /// <returns></returns>

        public void DeleteCart(ListView id)
        {
            try
            {
               cartdal.DeleteCart(id);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }
        public int FetchPrice()
        {
            int price;
            try
            {
                 price=cartdal.FetchPrice();
                
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
            return price;
        }
    
        public void ClearCart()
        {
            try
            {
                 cartdal.ClearCart();

            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
            
        }
    }
}
