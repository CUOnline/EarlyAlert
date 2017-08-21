using System.Collections.Generic;
using System.Data;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private const string CourseSql =
            "select distinct course_dim.id,canvas_id,course_dim.name from course_dim inner join enrollment_fact on enrollment_fact.course_id = course_dim.id " +
            "where account_id = {2} and course_dim.enrollment_term_id = {0} and wiki_id is not null and computed_final_score is not null and computed_current_score is not null and computed_current_score < {1} ORDER BY name";

        private const string InitialCourseSql =
            "select distinct course_dim.id,canvas_id,course_dim.name from course_dim inner join enrollment_fact on enrollment_fact.course_id = course_dim.id " +
            "where account_id = {1} and wiki_id is not null and computed_final_score is not null and computed_current_score is not null and computed_current_score < {0} and computed_current_score > 0 " +
            "and start_at < current_date and conclude_at > current_date ORDER BY name";

        public List<Courses> GetCourses(string termId, string score, string accountId)
        {
            var canvas = new CanvasRedShift();
            var canvasCourses = new List<Courses>();
            var sql = string.Format(CourseSql, termId, score, accountId);
            var course = canvas.GetCanvasData(sql);

            foreach (DataRow row in course.Tables[0].Rows)
            {
                var singleCourse = new Courses
                {
                    Id = row["id"].ToString(),
                    CanvasId = row["canvas_id"].ToString(),
                    Name = row["name"].ToString()
                };

                canvasCourses.Add(singleCourse);
            }

            return canvasCourses;
        }

        public List<Courses> GetInitialCourses(string score, string accountId)
        {
            var canvas = new CanvasRedShift();
            var canvasCourses = new List<Courses>();
            var sql = string.Format(InitialCourseSql, score, accountId);
            var course = canvas.GetCanvasData(sql);

            foreach (DataRow row in course.Tables[0].Rows)
            {
                var singleCourse = new Courses
                {
                    Id = row["id"].ToString(),
                    CanvasId = row["canvas_id"].ToString(),
                    Name = row["name"].ToString()
                };

                canvasCourses.Add(singleCourse);
            }

            return canvasCourses;
        }
    }
}