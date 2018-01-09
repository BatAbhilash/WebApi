using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Microsoft.Extensions.Options;
using WebApi.Services;

namespace WebApiCore2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private CustomerService _customerService;

        public CustomerController(IOptions<ConnectionStrings> connectionStrings)
        {
            _customerService = new CustomerService(connectionStrings.Value.DefaultConnection.ToString());
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.Get();
            if (customers != null)
                return Ok(customers);
            return BadRequest("No customers found");
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var customer = _customerService.Get(id);
            if (customer != null)
                return Ok(customer);
            return BadRequest("No Customer Found...!");
        }

    }
}