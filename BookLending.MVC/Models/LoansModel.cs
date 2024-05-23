using System.ComponentModel.DataAnnotations;

namespace BookLending.MVC.Models;

public class LoansModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Digite o nome do Recebedor")]
    public string Recipient { get; set; } = string.Empty;
    [Required(ErrorMessage = "Digite o nome do Fornecedor")]
    public string Provider { get; set; } = string.Empty;
    [Required(ErrorMessage = "Digite o nome do Livro")]
    public string BorrowedBook { get; set; } = string.Empty;
    public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
}