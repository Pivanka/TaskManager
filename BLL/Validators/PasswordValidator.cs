using BLL.Validators.Contracts;
using System.Text.RegularExpressions;

namespace BLL.Validators
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly string _passwordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        public Regex _validatePasswordRegex;
        public PasswordValidator()
        {
            _validatePasswordRegex = new(_passwordPattern);
        }
        public bool IsPasswordValid(string password)
        {
            return _validatePasswordRegex.IsMatch(password);
        }
    }
}
