using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;
using Router = SchoolProject.Data.AppMetaData.Router;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class StudentController : AppControllerBase
    {

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await Mediator.Send(new GetStudentListQuery());
            return NewResult(response);
        }

        [HttpGet(Router.StudentRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        } 

        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetStudentByID([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetStudentByIDQuery(id));
            return NewResult(response);
        }
        [Authorize(Policy = "CreateStudent")]
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create(AddStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Policy = "EditStudent")]
        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit(EditStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Policy = "DeleteStudent")]
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
    }
}
