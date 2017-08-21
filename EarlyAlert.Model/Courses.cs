using System.Collections.Generic;

namespace EarlyAlert.Model
{
    public class Courses
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CanvasId { get; set; }
        public List<Students> CanvasStudents { get; set; }
    }
}