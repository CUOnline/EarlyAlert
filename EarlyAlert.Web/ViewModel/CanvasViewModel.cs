using System.Collections.Generic;
using EarlyAlert.Model;

namespace EarlyAlert.Web.ViewModel
{
    public class CanvasViewModel
    {
        public List<Courses> Courses { get; set; }
        public List<Students> Students { get; set; }
        public List<Term> Terms { get; set; }
        public Term CurrentTerm { get; set; }
        public Account CurrentAccount { get; set; }
        public string CurrentScore { get; set; }
        public bool Authorized { get; set; }
    }
}