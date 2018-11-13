using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShoppingStore.Entities
{
    public class Product
    {
        public string ProductID
        {
            get;
            set;
        }
        public string CategoryID
        {
            get;
            set;
        }
        public string ModelNumber
        {
            get;
            set;
        }
        public string ModelName
        {
            get;
            set;
        }
        public int UnitCost
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
    }
}
