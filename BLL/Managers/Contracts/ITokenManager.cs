using DAL.Models;

namespace BLL.Managers.Contracts
{
    public interface ITokenManager
    {
        string GenerateToken(User user);
    }
}
