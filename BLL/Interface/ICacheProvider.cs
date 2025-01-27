using Model;

namespace BLL.Interface
{
    public interface ICacheProvider
    {
        Task<IEnumerable<Employee>> GetCachedResponse();
    }
}
