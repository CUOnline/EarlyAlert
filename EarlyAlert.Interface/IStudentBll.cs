using System.Collections.Generic;
using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface IStudentBll : IBll<Students>
    {
        List<Students> GetStudentsforCourse(string courseId, string score, string accountId);
    }
    
}
