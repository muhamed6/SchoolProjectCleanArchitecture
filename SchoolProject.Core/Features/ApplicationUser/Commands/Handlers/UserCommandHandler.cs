using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,

        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>


    {

        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion


        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }




        #endregion

        #region Handle Functions

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user != null) 
             return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
            
            var userByUserName = await _userManager.FindByNameAsync(request.UserName);

            if (userByUserName != null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsExist]);

            var identityUser = _mapper.Map<User>(request);

            var createResult = await _userManager.CreateAsync(identityUser, request.Password);

            if(!createResult.Succeeded)
             return BadRequest<string>(createResult.Errors.FirstOrDefault().Description);

            return Created("");

        }


        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if(oldUser is null)
                return NotFound<string>();

            var newUser =  _mapper.Map(request,oldUser);

            var result = await _userManager.UpdateAsync(newUser);

            if(!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);

            return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);

        }


        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
                return NotFound<string>();

            var result = await _userManager.DeleteAsync(user);

            if(!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeletedFailed]);

            return Success<string> (_stringLocalizer[SharedResourcesKeys.Deleted]);

        }


    }
}
