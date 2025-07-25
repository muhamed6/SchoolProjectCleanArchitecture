﻿using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIDMapping()
        {
            CreateMap<Department, GetDepartmentByIDResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor.Localize(src.Instructor.ENameAr, src.Instructor.ENameEn)))
                .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors));
            

            CreateMap<DepartmentSubject, SubjectResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));



               CreateMap<Instructor, InstructorResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.ENameAr, src.ENameEn)));


                
        }
    }
}
