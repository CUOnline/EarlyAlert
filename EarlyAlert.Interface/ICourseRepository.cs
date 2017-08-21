using System.Collections.Generic;
using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface ICourseRepository : IRepository<Courses>
    {
        List<Courses> GetCourses(string termId, string score, string accountId);
        List<Courses> GetInitialCourses(string score, string accountId);
    }
}
