using System.Collections.Generic;
using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface ICourseBll : IBll<Courses>
    {
        List<Courses> GetCourses(string termId, string score, string accountId);
        List<Courses> GetInitialCourses(string score, string accountId);
    }
}
