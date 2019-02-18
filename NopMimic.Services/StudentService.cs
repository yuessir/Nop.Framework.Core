using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NopMimic.Core.Data;
using NopMimic.Core.Domain.Students;

namespace NopMimic.Services
{
   

    public partial class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        public IList<Student> GetStudents()
        {
            return _studentRepository.Table.ToList();
        }
    }
}
