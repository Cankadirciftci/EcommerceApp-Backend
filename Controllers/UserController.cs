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
        public async Task<IActionResult> GetUser()
        {
            if (User.IsInRole(RoleType.Admin.ToString()))
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }

            return Unauthorized("Yalnızca admin kullanıcılar bu işlemi gerçekleştirebilir.");
        }


    }
}