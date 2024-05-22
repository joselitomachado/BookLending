namespace BookLending.MVC.Models;

public class LoansModel
{
    public int Id { get; set; }
    public string Recipient { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string BorrowedBook { get; set; } = string.Empty;
    public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
}