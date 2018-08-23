using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSManager_API.DB;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace KSManager_API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly Database _database;

        public UsersController(Database database)
        {
            _database = database;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _database.User;
        }

        // GET api/values/5
        [HttpGet("{id}", Name ="GetUser")]
        public IActionResult Get(Guid id)
        {
            var user = _database.User.Find(id);

            if (user != null)
                return Ok(user);
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            if (user != null)
            {
                _database.User.Add(user);
                await _database.SaveChangesAsync();
                return Ok(user.Id);
            }
            return BadRequest();
        }   

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]User value)
        {
            var user = _database.User.Find(id);
            if (user == null)
                return NotFound();

            if (!user.UserName.Equals(value.UserName) && value.UserName != null)
                user.UserName = value.UserName;

            if (!user.FirstName.Equals(value.FirstName) && value.FirstName != null)
                    user.FirstName = value.FirstName;

            if (!user.LastName.Equals(value.LastName) && value.LastName != null)
                    user.LastName = value.LastName;

            if (user.Birthday != value.Birthday && value.Birthday != DateTime.MinValue)
                    user.Birthday = value.Birthday;


            _database.User.Update(user);
            _database.SaveChanges();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _database.User.Find(id);
            if (user == null)
                return NotFound();

            _database.Remove(user);
            _database.SaveChanges();
            return NoContent();
        }
    }
}
