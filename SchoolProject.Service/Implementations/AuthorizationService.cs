using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Dtos;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService 
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager; 
        }


        #endregion



        #region Handle Functions

        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if(result.Succeeded) 
            {
                return "Success";
            }
            return "Failed";
        }

        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
         var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
                return "notfound";

            role.Name = request.Name;
          var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            //    var role = await _roleManager.FindByIdAsync(roleName);
            //    if (role == null) return false;

            //    return true;

            return await _roleManager.RoleExistsAsync(roleName);
        }
        #endregion
    }
}
