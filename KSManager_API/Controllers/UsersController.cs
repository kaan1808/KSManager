﻿using System;
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

        private readonly KsDatabase _ksDatabase;

        public UsersController(KsDatabase ksDatabase)
        {
            _ksDatabase = ksDatabase;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var user = _ksDatabase.User.Select(x => new User
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Username = x.Username,
                Birthday = x.Birthday,
                LastName = x.LastName
            });
            return user;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var user = _ksDatabase.User.Find(id);

            if (user != null)
                return Ok(new {user.Id, user.LastName, UserName = user.Username, user.FirstName, user.Birthday});
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            if (user.Id == Guid.Empty || user.Username == null || user.Salt == null || user.Password == null)
                return BadRequest();

            _ksDatabase.User.Add(user);
            await _ksDatabase.SaveChangesAsync();
            return Ok(user.Id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]User value)
        {
            var user = _ksDatabase.User.Find(id);
            if (user == null)
                return NotFound();

            if (!user.Username.Equals(value.Username) && value.Username != null)
                user.Username = value.Username;

            if (!user.FirstName.Equals(value.FirstName) && value.FirstName != null)
                    user.FirstName = value.FirstName;

            if (!user.LastName.Equals(value.LastName) && value.LastName != null)
                    user.LastName = value.LastName;

            if (user.Birthday != value.Birthday && value.Birthday != DateTime.MinValue)
                    user.Birthday = value.Birthday;


            _ksDatabase.User.Update(user);
            await _ksDatabase.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = _ksDatabase.User.Find(id);
            if (user == null)
                return NotFound();

            _ksDatabase.User.Remove(user);
            await _ksDatabase.SaveChangesAsync();
            return Ok();
        }
    }
}
