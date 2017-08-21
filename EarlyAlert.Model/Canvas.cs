using System.Collections.Generic;

namespace EarlyAlert.Model
{
    public class Canvas
    {
        public string CurrentTermId { get; set; }
        public string StudentScore { get; set; }
        public int TotalStudents { get; set; }
        public List<Term> CanvasTerms { get; set; }
        public List<Courses> CanvasCourses { get; set; }
    }
}