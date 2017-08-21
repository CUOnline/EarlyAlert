using System.Collections.Generic;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.BLL
{
    public class StudentBll : IStudentBll
    {
        private readonly IStudentRepository studentRepository;

        public StudentBll(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public List<Students> GetStudentsforCourse(string courseId, string score, string accountId)
        {
            return studentRepository.GetStudentsforCourses(courseId,score, accountId);
        }
    }
}

