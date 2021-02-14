using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session_State.Models
{
    public class ShoppingCart
    {
       public string id { get; set; }
       public List<KeyValuePair<string, string>> data { get; set; }
        
    }
}
