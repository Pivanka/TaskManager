namespace BLL.Validators.Contracts
{
    public interface IPasswordValidator
    {
        bool IsPasswordValid(string password);
    }
}
