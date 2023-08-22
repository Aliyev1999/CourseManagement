using CourseManagement.Interfaces;
using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Infrastructure
{
    public class AuthorizeActionFilter : IAsyncAuthorizationFilter
    {
        private string _permission;
        private string[] _permissionsSplit = _emptyArray;
        private string _roles;
        private string[] _rolesSplit = _emptyArray;

        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheService;
        private static readonly string[] _emptyArray = new string[0];

        public AuthorizeActionFilter(string permission, string role, ICurrentUserService currentUserService, ICacheService cacheService)
        {
            this._permission = permission;
            this._roles = role.ToString();
            this._currentUserService = currentUserService;
            this._cacheService = cacheService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext actionContext)
        {
            if (_currentUserService.UserId == 0)
            {
                actionContext.Result = new UnauthorizedResult();
            }

            if (HasAllowAnonymous(actionContext.Filters))
            {
                return;
            }

            if (string.IsNullOrEmpty(_permission) && string.IsNullOrEmpty(_roles))
            {
                return;
            }

            bool hasRole = await CheckUserRole(_roles);
            if (!hasRole)
            {
                actionContext.Result = new StatusCodeResult(401);
            }

            bool hasPermission = await CheckUserPermission(_permission);
            if (!hasPermission)
            {
                actionContext.Result = new StatusCodeResult(403);
            }
        }

        private static bool HasAllowAnonymous(IList<IFilterMetadata> filters)
        {
            for (var i = 0; i < filters.Count; i++)
            {
                if (filters[i] is IAllowAnonymousFilter)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<bool> CheckUserPermission(string permissionList)
        {
            _permissionsSplit = SplitString(permissionList);

            bool check = true;
            if (string.IsNullOrEmpty(permissionList))
                return true;

            var value = _cacheService.Get(_currentUserService.UserId.ToString());
            List<string> userPermisson = (List<string>)value;
            foreach (var item in _permissionsSplit)
            {
                if (!userPermisson.Any(x => x == item))
                {
                    return false;
                }
            }
            return check;
        }
        private async Task<bool> CheckUserRole(string roleList)
        {
            string role = _currentUserService.Role;
            _rolesSplit = SplitString(roleList);
            bool check = true;
            if (string.IsNullOrEmpty(roleList))
                return true;

            if (_rolesSplit.FirstOrDefault(x => x == role) == null)
            {
                check = false;
            }

            return check;
        }
        internal static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return _emptyArray;
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }
    }
}

