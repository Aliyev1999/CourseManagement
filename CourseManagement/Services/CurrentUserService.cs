using CourseManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CourseManagement.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public long? UserId { get; set; }
        public string Role { get; set; }
        public List<string> Permissions { get; set; }

        private IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            var userId = this._httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = this._httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
            var permissionsJson = this._httpContextAccessor.HttpContext?.User?.FindFirstValue($"Permissions{userId}");
            if (permissionsJson != null)
            {
                Permissions = new List<string>(JsonConvert.DeserializeObject<List<string>>(permissionsJson));
            }
            UserId = Convert.ToInt64(userId);
            Role = userRole;
        }
    }
}
