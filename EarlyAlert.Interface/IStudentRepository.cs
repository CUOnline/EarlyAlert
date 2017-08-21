using System.Collections.Generic;
using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface IStudentRepository : IRepository<Students>
    {
        List<Students> GetStudentsforCourses(string courseId, string score, string accountId);
    }
}
