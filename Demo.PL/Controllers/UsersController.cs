using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UsersController( UserManager<ApplicationUser> userManager , 
			SignInManager<ApplicationUser> signInManager , IMapper mapper )
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index( string SearchValue)
		{
			if (string.IsNullOrEmpty(SearchValue))
			{

				var users = await _userManager.Users.ToListAsync();
				var usersMapped = _mapper.Map<IEnumerable<UserViewModel>>(users);

				//var users = await _userManager.Users.Select(u => new UserViewModel
				//{
				//	Id = u.Id,
				//	Email = u.Email,
				//	Fname = u.FirstName,
				//	LName = u.LastName,
				//	PhoneNumber = u.PhoneNumber,
				//	Roles = _userManager.GetRolesAsync(u).Result
				//}).ToListAsync() ;

				return View(usersMapped);

			}
			else
			{
				var user = await _userManager.FindByEmailAsync(SearchValue);

				var userViewMapped = new UserViewModel
				{
					Id = user.Id,
					Email = user.Email,
					Fname = user.FirstName,
					LName = user.LastName,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};
					return View(new List<UserViewModel> { });
			}

		}
	
	
		public async Task<IActionResult> Details( string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			var userViewMapped = _mapper.Map<UserViewModel>(user);
			return View(userViewMapped); 
		}

		public async Task<IActionResult> Edit(string Id)
		{
			var user = await _userManager.FindByIdAsync(Id);

			var userMapped = _mapper.Map<UserViewModel>(user);
			return View(userMapped);
		}
		[HttpPost]
		public async Task<IActionResult> Edit( string Id , UserViewModel model)
		{
			if (Id != model.Id)
				return BadRequest();
		
			if (ModelState.IsValid)
			{
				try
				{
                    var user = await _userManager.FindByIdAsync(Id);

                    var userInDb = _mapper.Map<ApplicationUser>(model);

                    await _userManager.UpdateAsync(userInDb);

                    return RedirectToAction(nameof(Index));
                }
				catch (Exception  ex) 
				{

					ModelState.AddModelError(string.Empty, ex.Message);
				}
            }

			return View(model);
		}
			


	}
}
