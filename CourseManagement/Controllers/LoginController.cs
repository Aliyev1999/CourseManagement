using CourseManagement.Helper;
using CourseManagement.Models;
using CourseManagement.Services;
using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        public LoginController(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetBy(x => (x.Username == model.Username));
                List<UserPermission> userPermis;
                if (user != null)
                {
                    userPermis = await _unitOfWork.UserPermissionRepository.GetAll().Include(x => x.Permission).Where(up => up.UserID == user.ID).ToListAsync();

                    List<string> permissions = HelperMethods.GetPermissionStringList(userPermis);

                    var hashPassword = SeurityHash.Hash(model.Password);
                    var result = SeurityHash.Verify(model.Password, user.Password);
                    if (result)
                    {
                        Role role = await _unitOfWork.RoleRepository.GetBy(x => (x.ID == user.RoleID));
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, role.Name),
                            new Claim(ClaimTypes.Name, user.Name.ToString()),
                            new Claim(ClaimTypes.NameIdentifier, (string)user.ID.ToString())
                         };

                        var claimIdentity = new ClaimsIdentity(claims, "Login");
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                        CacheRequest cacheRequest = new CacheRequest()
                        {
                            key = user.ID.ToString(),
                            value = permissions
                        };

                        _cacheService.Set(cacheRequest);

                        if (string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
