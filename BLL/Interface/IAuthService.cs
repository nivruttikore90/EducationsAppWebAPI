using Model.Core;
using Model;

namespace BLL.Interface
{
    public interface IAuthService
    {
        string GenerateJwtAuthToken(string? userName, int? userId);
        TransactionRequest<LoginInfo> Login(LoginInfo loginInfo);
    }
}
