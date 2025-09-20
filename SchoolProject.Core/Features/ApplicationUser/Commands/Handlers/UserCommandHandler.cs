using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
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

        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>



    {

        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IApplicationUserService _applicationuserService;
        #endregion


        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService, IApplicationUserService applicationuserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _applicationuserService = applicationuserService;
        }




        #endregion

        #region Handle Functions

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<User>(request);

            var createResult = await _applicationuserService.AddUserAsync(identityUser, request.Password);

            switch (createResult)
            {
                case "EmailIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
                   
                case "UserNameIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsExist]);
                   
                   
                case "ErrorInCreateUser":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToAddUser]);
                   
                   
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryToRegisterAgain]);

                case "Success":
                    return Success<string>("");

                default:
                    return BadRequest<string>(createResult);
            }
        }


        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser is null)
                return NotFound<string>();

            var newUser = _mapper.Map(request, oldUser);

            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Id != newUser.Id);

            if(userByUserName != null)
       return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsExist]);

            var result = await _userManager.UpdateAsync(newUser);

            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);

            return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);

        }


        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
                return NotFound<string>();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeletedFailed]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);

        }
       




        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
                return NotFound<string>();

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if(!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[result.Errors.FirstOrDefault().Description]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);

        }
        #endregion
    }
}
