using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSManager_API.DB;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KSManager_API.Controllers
{
    [Route("api/[controller]")]
    public class PasswordStorageDatasController : Controller
    {
        private readonly Database _database;

        public PasswordStorageDatasController(Database database)
        {
            _database = database;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<PasswordStorageData> Get()
        {
            return _database.PasswordStorageDatas;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var entry = _database.PasswordStorageDatas.Find(id);

            if (entry != null)
                return Ok(entry);

            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PasswordStorageData value)
        {
            if (value.Id == Guid.Empty || value.User.Id == Guid.Empty || value.Title == null ||
                value.LastChanges == DateTime.MinValue) return BadRequest();

            _database.PasswordStorageDatas.Add(value);
            await _database.SaveChangesAsync();
            return Ok(value.Id);

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PasswordStorageData value)
        {
            await _database.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var entry = _database.PasswordStorageDatas.Find(id);
            if (entry == null)
                return NotFound();

            _database.PasswordStorageDatas.Remove(entry);
            _database.SaveChanges();
            return Ok();
        }
    }
}
