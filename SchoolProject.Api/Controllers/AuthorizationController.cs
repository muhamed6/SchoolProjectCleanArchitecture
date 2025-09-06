using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Results;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.Authorization.Create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(Router.Authorization.Edit)]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.Authorization.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(result);
        }



        [HttpGet(Router.Authorization.RoleList)]
        public async Task<IActionResult> GetRoleList()
        {
            var result = await Mediator.Send(new GetRolesListQuery());
            return NewResult(result);
        }

        [SwaggerOperation(Summary ="الصلاحية عن طريق ال id", OperationId = "RoleById")]
        [HttpGet(Router.Authorization.GetRoleById)]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetRoleByIdQuery{Id = id});
            return NewResult(result);
        }

        [SwaggerOperation(Summary ="ادارة صلاحيات المستخدمين", OperationId = "ManageUserRoles")]
        [HttpGet(Router.Authorization.ManageUserRoles)]
        public async Task<IActionResult> GetManageUserRoles([FromRoute] int userId)
        {
            var result = await Mediator.Send(new ManageUserRolesQuery { UserId = userId });
            return NewResult(result);
        }


        [SwaggerOperation(Summary = " تعديل صلاحيات المستخدمين", OperationId = "UpdateUserRoles")]
        [HttpPut(Router.Authorization.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [SwaggerOperation(Summary = "ادارة صلاحيات الاستخدام", OperationId = "ManageUserClaims")]
        [HttpGet(Router.Authorization.ManageUserClaims)]
        public async Task<IActionResult> GetManageUserClaims([FromRoute] int userId)
        {
            var result = await Mediator.Send(new ManageUserClaimsQuery { UserId = userId });
            return NewResult(result);
        }


        [SwaggerOperation(Summary = " تعديل صلاحيات الاستخدام للمستخدمين", OperationId = "UpdateUserClaims")]
        [HttpPut(Router.Authorization.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
