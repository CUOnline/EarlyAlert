using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.RedShift
{
    public class CanvasRepository : ICanvasRepository
    {
        private const string TermSql = "select id,name from enrollment_term_dim";
        private const string CourseSql =
            "select * from course_dim where account_id = 10430000000000016 and enrollment_term_id = 10430000000000081 and wiki_id is not null ORDER BY name";
        private  const string Studentssql =
            "select course_dim.name,computed_current_score,computed_final_score,user_dim.name  from course_dim " +
            "INNER JOIN enrollment_fact on course_dim.id = enrollment_fact.course_id " +
            "INNER JOIN user_dim on user_dim.id = enrollment_fact.user_id where account_id = 10430000000000016 " +
            "and course_dim.enrollment_term_id = 10430000000000081 and wiki_id is not null and computed_final_score is not null " +
            "and computed_current_score is not null ORDER BY course_dim.name";


        public IEnumerable<Students> GetStudents(string termId)
        {
            CanvasRedShift canvas = new CanvasRedShift();
            DataSet accounts = canvas.GetCanvasData(Studentssql);
            Students CanvasStudents = new Students();
            foreach (var rows in accounts.Tables[0].Rows)
            {
                
            }
            ReturnMessage CanvasStudents;
        }

       
    }
}
