using BLL.Dtos;

namespace BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterDto user);
        Task<string> LoginUserAsync(LoginDto user);
    }
}
