using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {

        #region Fields

        private readonly IStudentRepository _studentRepository;

        #endregion


        #region Constructors

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

    

        #endregion


        #region Handle Functions

        public async Task<List<Student>> GetStudentsListAsync()
        {
           return await _studentRepository.GetStudentsListAsync();
        }
        public async Task<Student> GetStudentByIDWithIncludeAsync(int id)
        {
            var student = await _studentRepository.GetTableNoTracking()
                .Include(x => x.Department)
                .Where(x => x.StudId == id)
                .FirstOrDefaultAsync();
            return student;
        }

        public async  Task<string> AddAsync(Student student)
        {
            

            //Add Student
            _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameArExist(string nameAr)
        {
            //check If the Name Is Exist Or Not
            var student = _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(nameAr)).FirstOrDefault();
            if (student == null) return false;

            return true;

        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(name)&!x.StudId.Equals(id)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;

        }

        public async Task<string> EditAsync(Student student)
        {
          await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {

                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch 
            {

                await trans.RollbackAsync();
                return "Failed";
            }

        }

        public async Task<Student> GetByIDAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            return student;
        }

        public IQueryable<Student> GetStudentsQuerable()
        {
              return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search)
        {
          var querable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if(search != null)
            querable = querable.Where(x => x.NameAr.Contains(search)|| x.Address.Contains(search));


            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudId:
                    querable = querable.OrderBy(x => x.StudId);
                    break;
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.NameAr);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Department.DNameAr);
                    break;
                default:
                    querable = querable.OrderBy(x => x.StudId);
                    break;
            }


            return querable;

        }

        public async Task<bool> IsNameArExistExcludeSelf(string nameAr, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(nameAr) & !x.StudId.Equals(id)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;

        }

        public async Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn) & !x.StudId.Equals(id)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;

        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            var student = _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn)).FirstOrDefault();
            if (student == null) return false;

            return true;
        }

        public IQueryable<Student> GetStudentByDepartmentIdQuerable(int departmentId)
        { 
         return   _studentRepository.GetTableNoTracking().Where(x => x.DID.Equals(departmentId))  .AsQueryable();
        }

        #endregion
    }
}
