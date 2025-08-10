using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public  class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion


        #region Constructors

        public EditRoleValidator(IStringLocalizer<SharedResources> localizer
                               )
        {
            _localizer = localizer;
            ApplyCustomValidationsRules();
            ApplyValidationsRules();
        }

        #endregion



        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        }

        public void ApplyCustomValidationsRules()
        {
           
        }
        #endregion


    }
}
