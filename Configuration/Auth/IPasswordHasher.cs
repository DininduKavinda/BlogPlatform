namespace TaskManagement.Configuration.Auth
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword( string providedPassword, string hashedPassword);
    }
}