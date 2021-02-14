using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Session_State.Models;
using Session_State.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Session_State.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartRepository allCarts;

        public CartController()
        {
            allCarts = new CartRepository();
        }
        // GET: api/<CartController>
        [HttpGet]
        public List<ShoppingCart> Get()
        {
            return allCarts.GetAllSessions();
        }

    }
}
