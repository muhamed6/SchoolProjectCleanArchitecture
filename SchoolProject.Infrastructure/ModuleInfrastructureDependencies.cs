﻿using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services) 
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
