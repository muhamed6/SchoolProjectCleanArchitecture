using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace SchoolProject.Service.Implementations
{
       public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _dbContext;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager,
                                    UserManager<User> userManager,
                                    ApplicationDBContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }


        #endregion



        #region Handle Functions

        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return "Success";
            }
            return "Failed";
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            if (users != null || users.Count() > 0) return "Used";

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return "Success";

            var errors = string.Join("-", result.Errors);
            return errors;
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


        public async Task<ManageUserRolesResult> ManageUserRolesData(User user)
        {
            var response = new ManageUserRolesResult();
            var rolesList = new List<UserRoles>();
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name    ))
                {
                    userRole.HasRole = true;
                }
                else
                {
                    userRole.HasRole = false;
                }
                rolesList.Add(userRole);
            }
            response.UserRoles = rolesList;
            return response;

        }


        public async Task<Role> GetRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<List<Role>> GetRolesList()
        {
            return await _roleManager.Roles.ToListAsync();

        }

        public async Task<bool> IsRoleExistById(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return false;
            return true;
        }

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            //    var role = await _roleManager.FindByIdAsync(roleName);
            //    if (role == null) return false;

            //    return true;

            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {

            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {


                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user is null)
                    return "UserIsNull";

                var userRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                {
                    return "FaildToRemoveOldRoles";
                }

                var selectedRoles = request.UserRoles.Where(x => x.HasRole == true).Select(x => x.Name);
                var addRolesResult = await _userManager.AddToRolesAsync(user, selectedRoles);

                if (!addRolesResult.Succeeded)
                {
                    return "FaildToAddNewRoles";
                }
                await transaction.CommitAsync();




                return "Success";
            }
            catch (Exception ex) 
            {
            await transaction.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }

        }
        public async Task<ManageUserClaimsResults> ManageUserClaimData(User user)
        {
            var response = new ManageUserClaimsResults();
            var userClaimsList = new List<UserClaims>();
            response.UserId = user.Id;

            var userClaims = await _userManager.GetClaimsAsync(user);

            foreach (var claim in ClaimsStore.Claims) 
            {
             var userClaim = new UserClaims();
                userClaim.Type = claim.Type;

                if(userClaims.Any(x => x.Type == claim.Type))
                {
                    userClaim.Value = true;
                }
                else
                {
                    userClaim.Value = false;
                }
                userClaimsList.Add(userClaim);

            }
            response.UserClaims = userClaimsList;
            return response;

        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                    return "UserIsNull";

                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);


                if (!removeClaimsResult.Succeeded)
                    return "FaildToRemoveOldClaims";
                
                var claims = request.UserClaims.Where(x => x.Value == true).Select(x => new Claim(type: x.Type, value: x.Value.ToString()));

                var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
                if(!addUserClaimResult.Succeeded)
                    return "FaildToAddNewClaims";


                await transaction.CommitAsync();

                return "Success";

            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync();
                return "FailedToUpdateUserClaims";

            }

        }
        #endregion

    }
}

