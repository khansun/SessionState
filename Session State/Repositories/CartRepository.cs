using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Session_State.Models;
using System.IO;
namespace Session_State.Repositories
{
    public class CartRepository
    {
        public List<ShoppingCart> GetAllSessions()
        {
            Console.WriteLine("load");
            var dataString = File.ReadAllText("Sessions/ShoppingCart.json");
            return JsonConvert.DeserializeObject<List<ShoppingCart>>(dataString);
        }
    }
}
