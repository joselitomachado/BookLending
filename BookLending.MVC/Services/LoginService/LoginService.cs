using BookLending.MVC.Data;
using BookLending.MVC.DTOs;
using BookLending.MVC.Models;
using BookLending.MVC.Services.PasswordService;

namespace BookLending.MVC.Services.LoginService;

public class LoginService : ILoginInterface
{
    private readonly BookLendingDbContext _bookLendingDb;
    private readonly IPasswordInterface _passwordInterface;

    public LoginService(BookLendingDbContext bookLendingDb, IPasswordInterface passwordInterface)
    {
        _bookLendingDb = bookLendingDb;
        _passwordInterface = passwordInterface;
    }

    public async Task<ResponseModel<UserModel>> UserRegister(UserRegisterDto userRegisterDto)
    {
        var response = new ResponseModel<UserModel>();

        try
        {
            if (EmailAlreadyExists(userRegisterDto))
            {
                response.Message = "E-mail já cadastrado.";
                response.Status = false;
                return response;
            }

            _passwordInterface.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new UserModel()
            {
                Name = userRegisterDto.Name,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _bookLendingDb.Users.Add(user);
            await _bookLendingDb.SaveChangesAsync();

            response.Message = "Usuário cadastrado com sucesso.";

            return response;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = false;
            return response;
        }
    }

    private bool EmailAlreadyExists(UserRegisterDto userRegisterDto)
    {
        var user = _bookLendingDb.Users.FirstOrDefault(x => x.Email == userRegisterDto.Email);

        if (user == null)
        {
            return false;
        }

        return true;
    }
}
