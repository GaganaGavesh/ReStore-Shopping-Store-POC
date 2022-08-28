using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]//This gives our class API powers
    [Route("api/[controller]")]//api/Products
    public class ProductsController : ControllerBase
    {
        //A readonly field cannot be assigned to (except in a constructor or a variable initializer)        
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        //Synchronous code, this blocks the thread when request comes in and any other request can't get dat since thread is blocked by first one
        //The solution is make these methods asynchronous.
        // [HttpGet] //api/Products
        // public ActionResult<List<Product>> GetProducts()//response is an Action result with the List of products
        // {
        //     var products = _context.Products.ToList();
        //     return Ok(products);
        // }

        //Asynchronous End Point
        [HttpGet] //api/Products
        public async Task<ActionResult<List<Product>>> GetProducts()//response is an Action result with the List of products
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // [HttpGet("{id}")] //api/Products/3
        // public ActionResult<Product> GetProduct(int id)
        // {
        //     var product = _context.Products.Find(id);
        //     return Ok(product);
        // }

        [HttpGet("{id}")] //api/Products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return Ok(product);
        }
    }
}