using System;
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

         
        } 
        public static class Authorization
        {
            public const string Prefix = Rule + "Authorization";
            public const string Create = Prefix + "/Role/Create";
            public const string Edit = Prefix + "/Role/Edit";
            public const string Delete = Prefix + "/Role/Delete/{id}";
            public const string GetRoleById = Prefix + "/Role/Get/{id}";
            public const string RoleList = Prefix + "/Role-List";
            public const string ManageUserRoles = Prefix + "/Manage-User-Roles/{userId}";
            public const string UpdateUserRoles = Prefix + "/Update-User-Roles";





        }
    }
}
