using BLL.Validators.Contracts;
using System.Text.RegularExpressions;

namespace BLL.Validators
{
    public class EmailValidator : IEmailValidator
    {
        private readonly string _emailPattern = "^\\S+@\\S+\\.\\S+$";
        public Regex _validateEmailRegex;
        public EmailValidator()
        {
            _validateEmailRegex = new(_emailPattern);
        }
        public bool IsEmailValid(string email)
        {
            return _validateEmailRegex.IsMatch(email);
        }
    }
}
