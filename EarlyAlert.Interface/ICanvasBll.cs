using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface ICanvasBll : IBll<Canvas>
    {
        Canvas GetStudents(string termId);
    }
}
