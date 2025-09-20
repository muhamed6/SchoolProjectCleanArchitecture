﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root+"/"+version+"/";
        public const string SingleRoute = "/{id}";
    
       public static class StudentRouting
        {
            public const string Prefix = Rule + "Student";
            public const string List = Prefix + "/List";
            public const string GetByID = Prefix + SingleRoute;
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/{id}";
            public const string Paginated = Prefix + "/Paginated";
        } 
        public static class DepartmentRouting
        {
            public const string Prefix = Rule + "Department";
            public const string List = Prefix + "/List";
            public const string GetByID = Prefix + "/Id";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/{id}";
            public const string Paginated = Prefix + "/Paginated";
        } 
        public static class ApplicationUserRouting
        {
            public const string Prefix = Rule + "User";
            //public const string List = Prefix + "/List";
            public const string GetByID = Prefix + "/{id}";
            public const string Create = Prefix + "/Create";

            public const string Edit = Prefix + "/Edit";

            public const string Delete = Prefix + "/{id}";

            //public const string Edit = Prefix + "/Edit";
            //public const string Delete = Prefix + "/{id}";

            public const string Paginated = Prefix + "/Paginated";
            public const string ChangePassword = Prefix + "/Change-Password";
        } 
        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";
            public const string SignIn = Prefix + "/SignIn";
            public const string RefreshToken = Prefix + "/Refresh-Token";
            public const string ValidateToken = Prefix + "/Validate-Token";
            public const string ConfirmEmail ="/Api/Authentication/ConfirmEmail";

         
        } 
        public static class Authorization
        {
            public const string Prefix = Rule + "AuthorizationRouting";
            public const string Roles = Prefix + "/Roles";
            public const string Claims = Prefix + "/Claims";
            public const string Create = Roles + "/Create";
            public const string Edit = Roles + "/Edit";
            public const string Delete = Roles + "/Delete/{id}";
            public const string RoleList = Roles + "/Role-List";
            public const string GetRoleById = Roles + "/Role-By-Id/{id}";
            public const string ManageUserRoles = Roles + "/Manage-User-Roles/{userId}";
            public const string ManageUserClaims = Claims + "/Manage-User-Claims/{userId}";
            public const string UpdateUserRoles = Roles + "/Update-User-Roles";
            public const string UpdateUserClaims = Claims + "/Update-User-Claims";
        } 
        public static class EmailsRoute
        {
            public const string Prefix = Rule + "EmailsRoute";
            public const string SendEmail = Prefix + "/SendEmail";
        }
    }
}
