using System.ComponentModel.DataAnnotations;

namespace BookLending.MVC.DTOs;

public class UserLoginDto
{
    [Required(ErrorMessage = "Digite o e-mail")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Digite a senha")]
    public string Password { get; set; } = string.Empty;
}
