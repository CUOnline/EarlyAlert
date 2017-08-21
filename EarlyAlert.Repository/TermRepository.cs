using System.Collections.Generic;
using System.Data;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.Repository
{
    public class TermRepository : ITermRepository
    {
        public List<Term> GetAllTerms()
        {
            var canvas = new CanvasRedShift();
            var canvasTerm = new List<Term>();
            var termSql = "SELECT Id, Name FROM Enrollment_Term_Dim ORDER BY Name";
            var terms = canvas.GetCanvasData(termSql);
            
            foreach (DataRow row in terms.Tables[0].Rows)
            {
                var singleTerm = new Term
                {
                    Id = row["id"].ToString(),
                    Name = row["name"].ToString()
                };

                canvasTerm.Add(singleTerm);
            }

            return canvasTerm;
        }
    }
}
