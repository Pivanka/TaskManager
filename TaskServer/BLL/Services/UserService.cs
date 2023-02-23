using BLL.Dtos;
using BLL.Managers.Contracts;
using BLL.Services.Contracts;
using BLL.Validators;
using BLL.Validators.Contracts;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<DAL.Models.User> _userRepository;
        private readonly IEmailValidator _emailValidator;
        private readonly IPasswordValidator _passwordValidator;
        private readonly ITokenManager _tokenManager;
        public UserService(IRepository<DAL.Models.User> userRepository,
            IPasswordValidator passwordValidator, IEmailValidator emailValidator,
            ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _emailValidator = emailValidator;
            _passwordValidator = passwordValidator;
            _tokenManager = tokenManager;
        }

        public async Task<string> LoginUserAsync(LoginDto user)
        {
            var userToLogin = await _userRepository.FirstOrDefaultAsync(u => u.Email == user.Email);

            if(userToLogin == null)
                throw new ArgumentException("User with this email doesn't register!");

            using var hmac = new HMACSHA512(userToLogin.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != userToLogin.PasswordHash[i]) 
                    throw new ArgumentException("Password is uncorrect!");
            }

            return _tokenManager.GenerateToken(userToLogin);
        }

        public async Task<string> RegisterUserAsync(RegisterDto user)
        {
            await Validate(user);

            using var hmac = new HMACSHA512();

            var userToRegister = new DAL.Models.User
            {
                Email = user.Email,
                UserName = user.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                PasswordSalt = hmac.Key
            };

            var registeredUser = await _userRepository.AddAsync(userToRegister);
            await _userRepository.SaveChangesAsync();

            return _tokenManager.GenerateToken(registeredUser);
        }

        private async Task Validate(RegisterDto user)
        {
            if (user.Password != user.ConfirmPassword)
                throw new ArgumentException("Passwords don't match");

            if (await IsRegister(user.Email))
                throw new ArgumentException("User with this email already registered!");

            if (!_emailValidator.IsEmailValid(user.Email))
                throw new ArgumentException("Incorrect email");

            if (!_passwordValidator.IsPasswordValid(user.Password))
                throw new ArgumentException("Weak password");
        }

        private async Task<bool> IsRegister(string email)
        {
            var customer = await _userRepository.Query()
                .FirstOrDefaultAsync(customer => customer.Email == email);
            return customer != null;
        }
    }
}
