using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;

        // AbstractValidator Da gwa package FluentValidation

        #region Fields

        #endregion


        #region Constructors
        public EditStudentValidator(IStudentService studentService)
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
            _studentService = studentService;
        }
        #endregion


        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("name Must Not Be Empty")
                .NotNull().WithMessage("name Must Not Be Null")
                .MaximumLength(100).WithMessage("Max Length is 10");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must Not Be Null")
                .MaximumLength(100).WithMessage("{PropertyName} Length is 10");
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (model,key, CancellationToken) => !await _studentService.IsNameArExistExcludeSelf(key,model.Id))
            .WithMessage("Name Is Exist");

          RuleFor(x => x.NameEn)
                .MustAsync(async (model,key, CancellationToken) => !await _studentService.IsNameEnExistExcludeSelf(key,model.Id))
            .WithMessage("Name Is Exist");
        }
        #endregion

    }
}
