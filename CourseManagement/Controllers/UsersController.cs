using CourseManagement.Helper;
using CourseManagement.Infrastructure;
using CourseManagement.Mappers;
using CourseManagement.Models;
using CourseManagement.Services;
using CourseManagementEFCore;
using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    [Authorize(permission: "", role: "Admin")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDBContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly ICacheService _cacheService;
        public UsersController(IUnitOfWork unitOfWork, AppDBContext context, ILogger<UsersController> logger, ICacheService cacheService)
        {
            this._unitOfWork = unitOfWork;
            this._context = context;
            this._logger = logger;
            this._cacheService = cacheService;
        }

        public async Task<IActionResult> Index(int pg = 1)
        {
            var userViewModel = new UserViewModel();
            var users = await _unitOfWork.UserRepository.GetAllList();
            var userList = new List<UserViewModel>();
            foreach (var item in users)
            {
                UserViewModel model = UserMapper.ViewMapper(item);
                userList.Add(model);
            }

            #region for pagination
            Pager pager = new Pager();
            var data = Pager.GetDataPerPage(userList, pg, ref pager);
            this.ViewBag.Pager = pager;
            #endregion

            userViewModel.Users = data;

            return View(userViewModel);
        }

        [AllowAnonimous]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("User Details : Information log : " + id.ToString() + " details");
            User tModel = await _unitOfWork.UserRepository.GetBy(x => (x.ID == id));
            Role role = await _unitOfWork.RoleRepository.GetBy(x => x.ID == tModel.RoleID);
            tModel.Role = role;
            UserViewModel model = UserMapper.ViewMapper(tModel);
            return View(model);
        }

        [Authorize(permission: "Create", role: "")]
        public async Task<IActionResult> Create(UserViewModel model)
        {


            //var result = SeurityHash.Verify(model.Password, hash);
            List<Role> roles = await _unitOfWork.RoleRepository.GetAllList();
            List<Permission> permissions = await _unitOfWork.PermissonRepository.GetAllList();
            model.Roles = roles;
            model.Permissions = new PermissionViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserMapper.DbMapper(model);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return View("Create", model);
            }

        }

        [Authorize(permission: "Update", role: "")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogError("User Edit : Error log");
            _logger.LogCritical("User Edit : Critical log");

            var user = await _unitOfWork.UserRepository.GetBy(x => (x.ID == id));
            var roles = await _unitOfWork.RoleRepository.GetAllList();

            var userPermissions = await _unitOfWork.UserPermissionRepository.GetAllListIncluding(x => x.UserID == user.ID);
            var permissionList = new List<Permission>();
            try
            {
                for (int i = 0; i < userPermissions.Count; i++)
                {
                    Permission perm = await _unitOfWork.PermissonRepository.GetBy(x => x.ID == userPermissions[i].PermissionID);
                    userPermissions[i].Permission = perm;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


            if (user == null)
            {
                Response.StatusCode = 404;
                _logger.LogWarning("User Edit : Warning log : " + id.ToString() + " 404 not found");
                return View("NotFound", id.Value);
            }
            user.UserPermissions = userPermissions;
            UserViewModel model = UserMapper.ViewMapper(user);
            model.Roles = roles;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {

            var hashPassword = SeurityHash.Hash(model.Password);
            model.Password = hashPassword;
            var userPe = await _unitOfWork.UserPermissionRepository.GetAllListIncluding(x => x.UserID == model.ID);
            var userPermissions = new List<UserPermission>();
            var permissionList = await _unitOfWork.PermissonRepository.GetAllList();

            foreach (var item in permissionList)
            {
                var up = new UserPermission();
                up.Permission = item;
                up.UserID = model.ID;
                up.PermissionID = item.ID;
                userPermissions.Add(up);
            }

            model.UserPermissions = userPermissions;
            var user = UserMapper.DbMapper(model);
            var forDbUP = new List<UserPermission>();
            forDbUP = user.UserPermissions.ToList();
            user.UserPermissions = new List<UserPermission>();
            _context.UserPermissions.RemoveRange(userPe);
            _context.UserPermissions.AddRange(forDbUP);
            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("User Edit : Information log :" + model.ID.ToString() + " updated");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return RedirectToAction("Index", "Users");

        }

        [Authorize(permission: "Delete", role: "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await _unitOfWork.UserRepository.GetBy(x => (x.ID == id));
            user.IsDeleted = true;
            await _cacheService.ClearCache(id);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Users");
        }
    }
}
