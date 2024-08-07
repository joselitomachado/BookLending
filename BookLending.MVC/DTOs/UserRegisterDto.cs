using System.ComponentModel.DataAnnotations;

namespace BookLending.MVC.DTOs;

public class UserRegisterDto
{
    [Required(ErrorMessage ="Digite o nome")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Digite o sobrenome")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Digite o email")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Digite a senha")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Digite a confirmação de senha"),
        Compare("Password", ErrorMessage = "As senha não estão iguais.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
