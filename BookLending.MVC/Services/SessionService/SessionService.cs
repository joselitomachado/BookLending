using BookLending.MVC.Models;
using Newtonsoft.Json;

namespace BookLending.MVC.Services.SessionService;

public class SessionService : ISessionInterface
{
    private readonly IHttpContextAccessor _contextAccessor;

    public SessionService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public UserModel GetSession()
    {
        var sessionUser = _contextAccessor.HttpContext.Session.GetString("sessionUser");

        if (string.IsNullOrEmpty(sessionUser))
            return null;

        return JsonConvert.DeserializeObject<UserModel>(sessionUser);
    }

    public void CreateSession(UserModel userModel)
    {
        var userJson = JsonConvert.SerializeObject(userModel);

        _contextAccessor.HttpContext.Session.SetString("sessionUser", userJson);
    }

    public void RemoveSession()
    {
        _contextAccessor.HttpContext.Session.Remove("sessionUser");
    }
}
