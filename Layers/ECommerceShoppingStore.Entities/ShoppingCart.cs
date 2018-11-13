﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShoppingStore.Entities
{
    public class ShoppingCart
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public DateTime DateCreated { get; set; }
    }  
}
