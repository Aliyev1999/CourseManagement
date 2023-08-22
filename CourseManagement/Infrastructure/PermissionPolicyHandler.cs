using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseManagement.Infrastructure
{
    public class PermissionPolicyHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionPolicyHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }
            try
            {
                string userId = context.User.Claims.FirstOrDefault(x => (x.Type == ClaimTypes.NameIdentifier)).Value;
                int id = Convert.ToInt32(userId);

                List<UserPermission> userPermission = await _unitOfWork.UserPermissionRepository.GetAllListIncluding(x => (x.UserID == id));
                var havePermision = userPermission.Any(x => x.PermissionID == requirement.Permission.ID);

                if (havePermision)
                {
                    context.Succeed(requirement);
                }
            }
            catch (Exception ex)
            {

            }
            //List<Permission> permissons = new List<Permission>();

            //foreach(var item in userPermission)
            //{
            //    Permission per =await _unitOfWork.PermissonRepository.GetBy(x => (x.ID == item.ID));
            //    permissons.Add(per);
            //}
        }
    }
}

