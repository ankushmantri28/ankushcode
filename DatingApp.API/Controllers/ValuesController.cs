using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context=context;
        }
        // GET: api/student
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            //throw new Exception("Test Exception");
            var values= await _context.Values.ToListAsync();
            return Ok(values);
        }

        // GET: api/student/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value=await _context.Values.FirstOrDefaultAsync(x=>x.Id==id);
            return Ok(value);
        }

        // POST: api/student
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/student/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/student/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}