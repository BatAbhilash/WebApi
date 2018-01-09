using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using WebApi.Models;
using WebApi.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore2.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {

        private readonly IOptions<BaseUrl> _baseUrl;
        private readonly IOptions<ConnectionStrings> _connectionStrings;
        public readonly IMediator _mediator;

        public ValuesController(IOptions<BaseUrl> baseUrl,IOptions<ConnectionStrings> connectionStrings, IMediator mediator) {
            _baseUrl = baseUrl;
            _connectionStrings = connectionStrings;
            _mediator = mediator;
        }

        // GET: api/values
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _mediator.Send(new Ping());
            string a = _baseUrl.Value.DefaultUrl;
            string z = _connectionStrings.Value.DefaultConnection;
            //return new string[] { "value1", "value2",result.ToString() };
            return Ok(result);
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,string query)
        {
            return Ok(new Value() { id = id, value = query });
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Value value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return CreatedAtAction("Get",new { id=value.id},value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Value {

        public int id { get; set; }

        [MinLength(3)]
        public string value { get; set; }
    }
}
