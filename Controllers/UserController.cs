using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EcommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetUser() {
            if (User.IsInRole(RoleType.Admin.ToString()))
            {
                var user = await _context.Users.ToListAsync();
                return Ok(user);
            }

            return Unauthorized("Yalnızca admin kullanıcılar bu işlemi gerçekleştirebilir.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserId([FromRoute] int id){
            if(User.IsInRole(RoleType.Admin.ToString())){
               var user = await _context.Users.FindAsync(id);
               if(user == null){
                return NotFound();
               }
               return Ok(user);
            }
            return Unauthorized("Yalnızca admin kullanıcılar bu işlemi gerçekleştirebilir.");
        }
        
        [HttpDelete("{id}")]

        public async Task<IActionResult> UserDelete([FromRoute] int id){
            if(User.IsInRole(RoleType.Admin.ToString())){
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if(user == null){
                    return NotFound();
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return Unauthorized("Yalnızca admin kullanıcılar bu işlemi gerçekleştirebilir.");
        }
    }
}