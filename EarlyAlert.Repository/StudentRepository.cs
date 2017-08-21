using System.Collections.Generic;
using System.Data;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private const string Studentssql =
           "select address,communication_channel_dim.user_id,enrollment_term_dim.id as TermId,enrollment_term_dim.name as TermName, course_dim.id as CourseId, course_dim.name as courseName,computed_current_score,computed_final_score,user_dim.name as studentName  " +
           "from course_dim INNER JOIN enrollment_fact on course_dim.id = enrollment_fact.course_id INNER JOIN user_dim on user_dim.id = enrollment_fact.user_id INNER JOIN communication_channel_dim on communication_channel_dim.user_id = enrollment_fact.user_id " +
           "INNER JOIN enrollment_term_dim  on enrollment_term_dim.id = course_dim.enrollment_term_id " +
           "where account_id = {2}  and wiki_id is not null and course_id = {0} and computed_final_score is not null and computed_current_score is not null and computed_current_score < {1} and position = 1 ORDER BY course_dim.name";

        public List<Students> GetStudentsforCourses(string courseId, string score, string accountId)
        {
            var canvas = new CanvasRedShift();
            var sql = string.Format(Studentssql, courseId, score, accountId);
            var accounts = canvas.GetCanvasData(sql);
            var canvasStudents = new List<Students>();

            foreach (DataRow row in accounts.Tables[0].Rows)
            {
                var cuStudents = new Students
                {
                    TermId = row["TermId"].ToString(),
                    TermName = row["TermName"].ToString(),
                    CourseId = row["CourseId"].ToString(),
                    CourseName = row["courseName"].ToString(),
                    CurrentScore = row["computed_current_score"].ToString(),
                    FinalScore = row["computed_final_score"].ToString(),
                    StudentName = row["studentName"].ToString(),
                    StudentEmail = row["address"].ToString()
                };

                canvasStudents.Add(cuStudents);
            }

            return canvasStudents;
        }
    }
}