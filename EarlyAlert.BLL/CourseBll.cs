using System.Collections.Generic;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.BLL
{
    public class CourseBll : ICourseBll
    {
        private readonly ICourseRepository courseRepository;

        public CourseBll(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public List<Courses> GetCourses(string termId, string score, string accountId)
        {
            return courseRepository.GetCourses(termId, score,accountId);
        }

        public List<Courses> GetInitialCourses(string score, string accountId)
        {
            return courseRepository.GetInitialCourses(score,accountId);
        }
    }
}
