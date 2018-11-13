using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceShoppingStore.Entities;

/// <summary>
/// AUTHOR:CHIRAG MEHTA
/// DATE:06/10/2018
/// PURPOSE:EXCEPTION LAYER
/// </summary>

namespace ECommerceShoppingStore.Exceptions
{

    /// <summary>
    /// EXCEPTIONS CLASS FOR CATCHING EXCEPTIONS
    /// </summary>


     public class ECommException :ApplicationException
     {
        public ECommException() : base()
        {

        }
        public ECommException(string message):base(message)
        {

        }
        public ECommException(string message,Exception innerException):
            base(message, innerException)
        {

        }
     }
}
