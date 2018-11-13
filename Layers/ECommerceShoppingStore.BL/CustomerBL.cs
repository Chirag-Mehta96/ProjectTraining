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
/// AUTHOR:DIPALI MALI
/// DATE:06/10/2018
/// PURPOSE:BL LAYER THE PRODUCTS
/// </summary>

namespace ECommerceShoppingStore.BL
{
    public class CustomerBL
    {
        CustomerDAL dal = null;

        /// <summary>
        /// Validations
        /// </summary>
        /// <param name="newData">A Customer</param>
        /// <returns>Boolean ValidateCustomer</returns>
        
        public bool ValidateCustomer(Customer newData)
        {
            StringBuilder sb = new StringBuilder();
            bool validData = true;

            //if (newData.CustomerFullName.ToString().Equals(string.Empty))
            //{
            //    validData = false;
            //    sb.Append(Environment.NewLine + "Customer Name cannot be blank.");
            //}

            if (newData.EmailId.ToString().Equals(string.Empty))
            {
                validData = false;
                sb.Append(Environment.NewLine + "Email field cannot be blank.");
            }

            if (newData.Password.ToString().Equals(string.Empty))
            {
                validData = false;
                sb.Append(Environment.NewLine + "Please enter your Password.");
            }

            if (newData.DeliveryAddress.ToString().Equals(string.Empty))
            {
                validData = false;
                sb.Append(Environment.NewLine + "Please give Delivery_Address.");
            }

            if (newData.CustomerFullName.Any(p => char.IsDigit(p)))
            {
                validData = false;
                sb.Append(Environment.NewLine + "Customer Name contains alphabets only.");
            }

            //if (!Regex.IsMatch(newData.Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            //{
            //    validData = false;
            //    sb.Append(Environment.NewLine + "Please enter valid email id.");
            //}

            //if (!Regex.IsMatch(newData.Password, @"^ (?=.*[a - z])(?=.*[A - Z])(?=.*\d)(?=.*[^\da - zA - Z]).{ 8,15}$"))
            //{
            //    validData = false;
            //    sb.Append(Environment.NewLine + "Please enter valid password.( String must be between 8 and 15 characters long. It must contain at least one number, one uppercase letter, one lowercase letter & one special character.)");
            //}

            if (validData == false)
                throw new ECommException(sb.ToString());
            return validData;
        }

        public CustomerBL(string conString)
        {
            dal = new CustomerDAL(conString);
        }

        /// <summary>
        /// To Add a Customer.
        /// </summary>
        
        public void Insert(Customer cust)
        {
            try
            {
                if (ValidateCustomer(cust))
                {
                    dal.Insert(cust);
                }
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// To Update a Customer.
        /// </summary>
        
        public void Update(Customer cust)
        {
            try
            {
                dal.Update(cust);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// To delete a Customer.
        /// </summary>
        
        public void Delete(int custId)
        {
            try
            {
                dal.Delete(custId);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// Get All Data of Customer.
        /// </summary>
        /// <returns>custs</returns>
       
        public List<Customer> SelectAll()
        {
            List<Customer> custs = new List<Customer>();
            try
            {
                custs = dal.SelectAll();
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
            return custs;
        }
    }
}
