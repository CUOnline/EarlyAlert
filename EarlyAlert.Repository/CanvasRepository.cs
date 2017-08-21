using System.Collections.Generic;
using System.Data;
using System.Linq;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.Repository
{
    public class CanvasRepository : ICanvasRepository
    {

        private const string TermSql = "select id,name from enrollment_term_dim";

        private const string CourseSql =
            "select course_dim.id,canvas_id,course_dim.name from course_dim where account_id = 10430000000000016 and enrollment_term_id = {0} and wiki_id is not null ORDER BY name";

        private const string Studentssql =
            "select enrollment_term_dim.id as TermId,enrollment_term_dim.name as TermName, course_dim.id as CourseId, course_dim.name as courseName,computed_current_score,computed_final_score,user_dim.name as studentName  " +
            "from course_dim INNER JOIN enrollment_fact on course_dim.id = enrollment_fact.course_id INNER JOIN user_dim on user_dim.id = enrollment_fact.user_id " +
            "INNER JOIN enrollment_term_dim  on enrollment_term_dim.id = course_dim.enrollment_term_id " +
            "where account_id = 10430000000000016 and course_dim.enrollment_term_id = {0} and wiki_id is not null and course_id = {1} and computed_final_score is not null and computed_current_score is not null and computed_final_score < 80 ORDER BY course_dim.name";

        public Canvas GetStudents(string termId)
        {
            var canvas = new Canvas();
            var canvasTerm = new List<Term>();
            var canvasCourses = new List<Courses>();
            var canvasRedShift = new CanvasRedShift();

            var termsSql = canvasRedShift.GetCanvasData(TermSql);
            var courseSql = string.Format(CourseSql, termId);
            var course = canvasRedShift.GetCanvasData(courseSql);

            foreach (DataRow row in termsSql.Tables[0].Rows)
            {
                var singleTerm = new Term
                {
                    Id = row["id"].ToString(),
                    Name = row["name"].ToString()
                };

                canvasTerm.Add(singleTerm);
            }

            foreach (DataRow dataRow in course.Tables[0].Rows)
            {
                var singleCourse = new Courses
                {
                    Id = dataRow["id"].ToString(),
                    CanvasId = dataRow["canvas_id"].ToString(),
                    Name = dataRow["name"].ToString()
                };

                var sql = string.Format(Studentssql, termId, singleCourse.Id);
                var accounts = canvasRedShift.GetCanvasData(sql);
                var canvasStudents = new List<Students>();

                foreach (DataRow row in accounts.Tables[0].Rows)
                {
                    var CUStudents = new Students
                    {
                        TermId = row["TermId"].ToString(),
                        TermName = row["TermName"].ToString(),
                        CourseId = row["CourseId"].ToString(),
                        CourseName = row["courseName"].ToString(),
                        CurrentScore = row["computed_current_score"].ToString(),
                        FinalScore = row["computed_final_score"].ToString(),
                        StudentName = row["studentName"].ToString()
                    };

                    canvasStudents.Add(CUStudents);
                }

                if (canvasStudents.Any())
                {
                    singleCourse.CanvasStudents = canvasStudents;
                    canvasCourses.Add(singleCourse);
                }
            }

            canvas.CanvasCourses = canvasCourses;
            canvas.CanvasTerms = canvasTerm;
            canvas.CurrentTermId = termId;

            return canvas;
        }
    }
}
