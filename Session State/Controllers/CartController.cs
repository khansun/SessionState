using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Session_State.Models;
using Session_State.Repositories;


namespace Session_State.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly CartRepository allCarts;

        public CartController()
        {
            allCarts = new CartRepository();
        }
        // GET: <CartController>
        [HttpGet]
        public List<ShoppingCart> Get()
        {
            return allCarts.GetAllSessions();
        }
        [HttpPost]
        [Route("add/{item}")]
        public String Add([FromRoute] string item)
        {
            string session_id = Request.Headers["session-id"];
            if (session_id == null)
            {
                Console.WriteLine("Session running");
                Guid id = allCarts.AddNewCart(item);
                Response.Headers.Add("session-id", id.ToString());
            }
            else
            {
                allCarts.AddToCart(Guid.Parse(session_id), item);
            }
            return "Item added to Shopping Cart";
        }

        [HttpDelete]
        [Route("remove/{item}")]
        public String Remove([FromRoute] string item)
        {
            string session_id = Request.Headers["session-id"];
            return allCarts.RemoveCartItem(Guid.Parse(session_id), item);
        }

        [HttpDelete]
        [Route("decrease/{item}")]
        public String Decrease([FromRoute] string item)
        {
            string session_id = Request.Headers["session-id"];
            return allCarts.DecreaseCartItem(Guid.Parse(session_id), item);
        }
    }
}
