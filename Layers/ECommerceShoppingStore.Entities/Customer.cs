using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Author: Chirag Mehta
/// Purpose: ENTITY LAYER FOR THE CUSTOMER
/// Date : 22nd September,2018
/// </summary>
 
namespace ECommerceShoppingStore.Entities
{
    /// <summary>
    /// CLASS FOR VARIOUS ATTRIBUTES AND PROPERTIES OF A CUSTOMER
    /// </summary>

    public class Customer
    {
        
        public int CustomerId {
            get;
            set;
        }
        [Required]
        public string CustomerFullName
        {
            get;
            set;
        }
        public string EmailId
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string DeliveryAddress
        {
            get;
            set;
        }

        /// <summary>
        /// DEFAULT CONSTRUCTOR TO INITIALIZE THE PROPERTIES/ATTRIBUTES TO THE DEFAULT VALUE
        /// </summary>
        
        public Customer()
        {
            CustomerId = 0;
            CustomerFullName = string.Empty;
            EmailId = string.Empty;
            Password = string.Empty;
            DeliveryAddress = string.Empty;
        }
    }
}
