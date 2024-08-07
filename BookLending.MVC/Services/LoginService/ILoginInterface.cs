using BookLending.MVC.DTOs;
using BookLending.MVC.Models;

namespace BookLending.MVC.Services.LoginService;

public interface ILoginInterface
{
    Task<ResponseModel<UserModel>> UserRegister(UserRegisterDto userRegisterDto);
}
