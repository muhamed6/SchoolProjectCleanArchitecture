using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion


        #region Constructors

        public AddRoleValidator( IStringLocalizer<SharedResources> localizer
                               , IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
            ApplyCustomValidationsRules();
            ApplyValidationsRules();
        }

        #endregion



        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
             
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.RoleName)
                 .MustAsync(async (key, CancellationToken) => !await _authorizationService.IsRoleExist(key))
             .WithMessage(_localizer[SharedResourcesKeys.IsExist]);

        }
        #endregion


    }
}
