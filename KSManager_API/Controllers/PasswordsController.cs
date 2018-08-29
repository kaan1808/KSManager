using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KSManager_API.DB;
using KSManager_API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KSManager_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PasswordsController : Controller
    {
        private readonly KsDatabase _database;

        public PasswordsController(KsDatabase database)
        {
            _database = database;
        }


        [HttpGet("small")]
        public async Task<ActionResult<IEnumerable<SmallPasswordEntry>>> GetSmall()
        {
            var userId = Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            var user = await _database.User.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return NotFound();

            var entries = _database.PasswordStorageDatas
                .Where(x => x.User == user && !x.IsDeleted)
                .Select(x => new SmallPasswordEntry
                {
                    Id = x.Id,
                    Title = x.Title,
                    Icon = x.Icon,
                    Username = x.Username
                });

            return Ok(await entries.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PasswordEntry>> Get(Guid id)
        {
            var userId = Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            var user = await _database.User.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return NotFound();

            var entry = await _database.PasswordStorageDatas
                .Where(x => x.User == user && x.Id == id && !x.IsDeleted)
                .Select(x => new PasswordEntry
                {
                    Id = x.Id,
                    Title = x.Title,
                    Icon = x.Icon,
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                    Note = x.Note,
                    SecurityAnswer = x.SecurityAnswer,
                    SecurityQuestion = x.SecurityQuestion,
                    Url = x.Url,
                    LastChanges = x.LastChanges
                })
                .SingleOrDefaultAsync();

            if (entry == null)
                return NotFound();

            return Ok(entry);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            var user = await _database.User.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return NotFound();

            var entry = await _database.PasswordStorageDatas.SingleOrDefaultAsync(
                x => x.User == user && x.Id == id && !x.IsDeleted);

            if (entry == null)
                return NotFound();

            entry.IsDeleted = true;
            await _database.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] PasswordEntry entry)
        {
            if (entry == null)
                return BadRequest(new { message = "Could not parse password entry" });
            
            var userId = Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = await _database.User.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return NotFound();

            if (string.IsNullOrEmpty(entry.Title))
                return BadRequest(new {message = "Title is missing"});
            if(entry.Id == Guid.Empty)
                return BadRequest(new { message = "Id is missing" });

            var oldEntry = await _database.PasswordStorageDatas.SingleOrDefaultAsync(x => x.User == user && x.Id == entry.Id && !x.IsDeleted);

            if (oldEntry == null)
                return NotFound(new {message = "Entry not found"});

            oldEntry.Title = entry.Title;
            oldEntry.Email = entry.Email;
            oldEntry.Icon = entry.Icon;
            oldEntry.Password = entry.Password;
            oldEntry.Url = entry.Url;
            oldEntry.Username = entry.Username;
            oldEntry.SecurityAnswer = entry.SecurityAnswer;
            oldEntry.SecurityQuestion = entry.SecurityQuestion;

            await _database.SaveChangesAsync();
            return Ok();
        }
    }
}
