using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_App_Clothing_Ecommerce.Models
{
    public class cart
    {
        public int poductid { get; set; }
        public string productName { get; set; }
        public int price { get; set; }
        public int qty { get; set; }
        public int bill { get; set; }

    }
}