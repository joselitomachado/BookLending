namespace BookLending.MVC.Services.PasswordService;

public interface IPasswordInterface
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
}
