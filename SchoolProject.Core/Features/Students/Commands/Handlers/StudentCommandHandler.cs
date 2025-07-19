using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>
                                                        , IRequestHandler<EditStudentCommand, Response<string>>
                                                        , IRequestHandler<DeleteStudentCommand, Response<string>>
    {

        #region Fields

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;


        #endregion

        #region Constructors

        public StudentCommandHandler(IStudentService studentService,
                                     IMapper mapper,
                                     IStringLocalizer<SharedResources> localizer
                                    ) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handle Functions

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);

            var result = await _studentService.AddAsync(studentMapper);

          
             if (result == "Success") return Created("");

            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIDAsync(request.Id);

            if(student == null) return NotFound<string>("Student Not Found");

            var studentMapper = _mapper.Map(request, student);

            var result = await _studentService.EditAsync(studentMapper);

            if (result == "Success") return Success("Edited Successfully");

            else return BadRequest<string>();


        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIDAsync(request.Id);

            if (student == null) return NotFound<string>("Student Not Found");

            var result = await _studentService.DeleteAsync(student);

            if (result == "Success") return Deleted<string>($"Deleted Successfully");

            else return BadRequest<string>();


        }

        #endregion

    }
}
