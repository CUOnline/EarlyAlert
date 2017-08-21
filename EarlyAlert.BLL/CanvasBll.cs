using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.BLL
{
    public class CanvasBll : ICanvasBll
    {

        private readonly ICanvasRepository canvasRepository;

        public CanvasBll(ICanvasRepository canvasRepository)
        {
            this.canvasRepository = canvasRepository;
        }

        public Canvas GetStudents(string termId)
        {
            return canvasRepository.GetStudents(termId);
        }
        
    }
}
