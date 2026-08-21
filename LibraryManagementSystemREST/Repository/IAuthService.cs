namespace LibraryManagmentSystem.Repository
{
    public interface IAuthService
    {
        string? Login(string username, string password);
    }
}
