using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IDepartmentService _departmenttService;
        // AbstractValidator Da gwa package FluentValidation

        #region Fields

        #endregion


        #region Constructors
        public AddStudentValidator( IStudentService studentService, IStringLocalizer<SharedResources> localizer, IDepartmentService departmenttService)
        {
            _localizer = localizer;
            _studentService = studentService;
            _departmenttService = departmenttService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();

            
        }
        #endregion


        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameArExist(key))
            .WithMessage(_localizer[SharedResourcesKeys.IsExist]);

            RuleFor(x => x.NameEn)
                .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameEnExist(key))
            .WithMessage(_localizer[SharedResourcesKeys.IsExist]);



            RuleFor(x => x.DepartmentId)
                .MustAsync(async (key, CancellationToken) => await _departmenttService.IsDepartmentIdExist(key.Value)) 
            .WithMessage(_localizer[SharedResourcesKeys.IsNotExist]);
        }
        #endregion

    }
}
