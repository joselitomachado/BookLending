using BookLending.MVC.Models;

namespace BookLending.MVC.Services.SessionService;

public interface ISessionInterface
{
    UserModel GetSession();
    void CreateSession(UserModel userModel);
    void RemoveSession();
}
