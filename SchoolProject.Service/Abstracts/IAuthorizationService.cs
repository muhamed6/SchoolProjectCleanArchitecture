using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
 public Task<string> AddRoleAsync(string roleName);
public Task<string> EditRoleAsync(EditRoleRequest request);
public Task<bool> IsRoleExistByName(string roleName);
public Task<bool> IsRoleExistById(int roleId);
public Task<string> DeleteRoleAsync(int roleId);
public Task<List<Role>> GetRolesList();
public Task<Role> GetRoleById(int id);
public Task<ManageUserRolesResult> ManageUserRolesData(User user);
public Task<string> UpdateUserRoles (UpdateUserRolesRequest request);
public Task<ManageUserClaimsResults> ManageUserClaimData(User user);
  public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);

    }
}
