using System;
using System.Collections.Generic;
using System.Text;
using NopMimic.Core.Domain.Students;

namespace NopMimic.Services
{
    public interface IStudentService
    {
        IList<Student> GetStudents();
    }
}
