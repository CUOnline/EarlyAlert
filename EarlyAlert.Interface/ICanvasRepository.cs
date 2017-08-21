using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface ICanvasRepository : IRepository<Canvas>
    {
        Canvas GetStudents(string termId);
    }
    
}
