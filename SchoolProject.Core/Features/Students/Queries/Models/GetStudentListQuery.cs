﻿using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentListResponse>>>
    {
        // IRequest da f package MediatR 
        // GetStudentListQuery da request , da List<Student> response 
    }
}
