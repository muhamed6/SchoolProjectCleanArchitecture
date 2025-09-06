using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>

    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly IAuthenticationService _authenticationService;


        #endregion

        #region Constructors

        public AuthenticationCommandHandler(
                                     IMapper mapper,
                                     IStringLocalizer<SharedResources> localizer,
                                     UserManager<User> userManager,
                                     SignInManager<User> signManager,
                                     IAuthenticationService authenticationService
                                    ) : base(localizer)
        {

            _mapper = mapper;
            _localizer = localizer;
            _userManager = userManager;
            _signManager = signManager;
            _authenticationService = authenticationService;
        }


        #endregion

        #region Handle Functions

        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest<JwtAuthResult>(_localizer[SharedResourcesKeys.UserNameIsNotExist]);



            var signInResult = await _signManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.Succeeded)
            {

                return BadRequest<JwtAuthResult>(_localizer[SharedResourcesKeys.PasswordNotCorrect]);

            }

            var result = await _authenticationService.GetJWTToken(user);

            return Success(result);



        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);

            switch(userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.UnAuthorized]);
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.TokenIsNotExpired]);
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.RefreshTokenIsExpired]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
            {
             return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }



        #endregion
    }
}
