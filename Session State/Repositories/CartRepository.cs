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
            Console.WriteLine("Getting all sessions for Shopping Carts");
            var dataString = File.ReadAllText("Sessions/ShoppingCart.json");
            return JsonConvert.DeserializeObject<List<ShoppingCart>>(dataString);
        }
        private void SetSessionData(List<ShoppingCart> carts)
        {
            Console.WriteLine("Session Data Added");
            var dataString = JsonConvert.SerializeObject(carts, Formatting.Indented);
            File.WriteAllText("Sessions/ShoppingCart.json", dataString);
        }

        public void AddToCart(Guid id, string item)
        {
            bool newItem = true;
            var carts = GetAllSessions();
            var cart = carts.Find(cart => cart.Session_id == id);
            foreach (var product in cart.Data.ToList())
            {
                if (product.Key == item)
                {
                    cart.Data.Remove(product);
                    cart.Data.Add(new KeyValuePair<string, int>(item, product.Value + 1));
                    newItem = false;
                }
            }
            if (newItem)
            {
                cart.Data.Add(new KeyValuePair<string, int>(item, 1));
            }

            SetSessionData(carts);
        }

        public Guid AddNewCart(string item)
        {
            ShoppingCart newCart = new ShoppingCart
            {
                Session_id = Guid.NewGuid()
            };
            var newItem = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>(item, 1)
            };
            newCart.Data = newItem;
            var carts = GetAllSessions();
            carts.Add(newCart);
            Console.WriteLine("New Cart data added to session");
            SetSessionData(carts);

            return newCart.Session_id;
        }

     
        public String RemoveCartItem(Guid id, string item)
        {
            var carts = GetAllSessions();
            var cart = carts.Find(cart => cart.Session_id == id);
            bool lostItem = true;
            foreach (var product in cart.Data.ToList())
            {
                if (product.Key == item)
                {
                    cart.Data.Remove(product);
                    Console.WriteLine("Item Removed from Shopping Cart");
                    lostItem = false;
                }
            }
            if (lostItem)
                return "There is no item of that type in the Shopping Cart";
            SetSessionData(carts);
            return "Item Removed from Shopping Cart";
        }

        public String DecreaseCartItem(Guid id, string item)
        {
            var carts = GetAllSessions();
            var cart = carts.Find(cart => cart.Session_id == id);
            bool missingItem = true;
            foreach (var product in cart.Data.ToList())
            {
                if (product.Key == item)
                {
                    if (product.Value == 0)
                        return "Shopping Cart has Zero amount of that item";
                    cart.Data.Remove(product);
                    cart.Data.Add(new KeyValuePair<string, int>(item, product.Value - 1));
                    Console.WriteLine("Item count Decreased");
                    missingItem = false;
                }
            }
            if (missingItem)
                return "There is no item of that type in the Shopping Cart";
            SetSessionData(carts);
            return "Item count Decreased";
        }
    }
}
