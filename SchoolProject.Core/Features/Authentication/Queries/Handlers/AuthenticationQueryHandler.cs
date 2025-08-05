using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
       IRequestHandler<AuthorizeUserQuery, Response<string>>

    {

        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthenticationService _authenticationService;


        #endregion

        #region Constructors

        public AuthenticationQueryHandler(
                                     IStringLocalizer<SharedResources> localizer,
                                     
                                     IAuthenticationService authenticationService
                                    ) : base(localizer)
        {
            _localizer = localizer;
            _authenticationService = authenticationService;
        }

       public  async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>(_localizer[SharedResourcesKeys.TokenIsExpired]);
        }


        #endregion

        #region Handle Functions




        #endregion
    }
}
