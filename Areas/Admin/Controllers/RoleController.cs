using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ShopingStore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NuGet.Versioning;
using ShopingStore.Data;

namespace ShopingStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {

        private readonly MyDBContext _myDBContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RoleController(MyDBContext myDBContext, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _myDBContext = myDBContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _myDBContext.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("NotFound");
            }
            var UserRole = await _userManager.GetRolesAsync(user);

            var EditVM = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = (List<string>)UserRole
            };
            return View(EditVM);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(EditUserViewModel editUserVM)
        {
            var user = await _userManager.FindByIdAsync(editUserVM.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User With Id ={editUserVM.Id} Can not be Found";
                return RedirectToAction("NotFound");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    user.UserName = editUserVM.UserName;
                    user.Email = editUserVM.Email;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListUsers");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);

                        }
                    }
                }
            }
            return View(editUserVM);
        }

        public IActionResult Index()
        {
            var Roles = _myDBContext.Roles.ToList();
            return View(Roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                ModelState.AddModelError("", "Role Is Already Exist");
                return View("Index", "Role");
            }
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = role.Name
            });
            return RedirectToAction("Index", "Role");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id ={id} Can not be Found";
                return RedirectToAction("NotFound");
            }

            var EditVM = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    EditVM.Users.Add(user.UserName);
                }
            }
            return View(EditVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel role, string Id)
        {
            var UpRole = _myDBContext.Roles.Find(Id);
            UpRole.Name = role.RoleName;
            UpRole.NormalizedName = role.RoleName?.ToUpper();
            var result = await _roleManager.UpdateAsync(UpRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(role);

        }

        [HttpPost]

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = _myDBContext.Roles.Find(id);
            if (role == null)
            {
                ModelState.AddModelError("", "this role not found");
                return View("Index", "Role");
            }
            else
            {
                var result = _myDBContext.UserRoles.Where(x => x.RoleId == id).Count();
                if (result > 0)
                {
                    TempData["Error"] = "THis Role Is Can not Deleted, Assigned To User";
                    return RedirectToAction("Index");
                }
                var DeletdRole = await _roleManager.DeleteAsync(role);
                if (DeletdRole.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");

            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id ={roleId} Can not be Found";
                return RedirectToAction("Edit", new { Id = roleId });
            }

            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);

            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        {

            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id ={roleId} Can not be Found";
                return RedirectToAction("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(model[i].IsSelected) && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("Edit", new { Id = roleId });
                }

            }
            return RedirectToAction("Edit", new { Id = roleId });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}