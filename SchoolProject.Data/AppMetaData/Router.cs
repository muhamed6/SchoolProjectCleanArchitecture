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
            //public const string Edit = Prefix + "/Edit";
            //public const string Delete = Prefix + "/{id}";
            public const string Paginated = Prefix + "/Paginated";
        }
    }
}
