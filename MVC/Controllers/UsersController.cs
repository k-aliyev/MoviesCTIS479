using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers
{
    public class UsersController : Controller
    {
        IUserService _userService;
        IRoleService _roleService;
        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Authorize]
        public IActionResult List()
        {
           
            List<UserModel> userList = _userService.Query().ToList();
            return View("List", userList); 
        }

        [HttpGet("Users/{action}")]
        public IActionResult Login()
        {
            return View(); 
        }

        
        [HttpPost("Users/{action}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel user)
        {
            
            var existingUser = _userService.Query().SingleOrDefault(u => u.Name == user.Name && u.Password == user.Password);
            if (existingUser is null) 
            {
                ModelState.AddModelError("", "Invalid user name and password!"); 
                return View(); 
            }

            List<Claim> userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, existingUser.Name),
                new Claim(ClaimTypes.Role, existingUser.RoleNameOutput)
            };


            var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction(nameof(List));
        }

        [HttpGet("Users/{action}")]
        public async Task<IActionResult> Logout()
        {
           
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction(nameof(List));
        }

        
        [HttpGet("Users/{action}")]
        public IActionResult AccessDenied()
        {
            return View("_Error", "You don't have access to this operation!");
        }


		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Roles = new SelectList(_roleService.Query().ToList(), "Id", "Name");

			return View();
		}

		[HttpPost] 
		[ValidateAntiForgeryToken] 
		public IActionResult Create(UserModel user) 
		{

			if (ModelState.IsValid) 
			{
				_userService.Add(user);
                return RedirectToAction(nameof(List));

            }

			ViewBag.Roles = new SelectList(_roleService.Query().ToList(), "Id", "Name");
			return View(user);
		}


	}
}
