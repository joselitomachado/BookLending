using BookLending.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLending.MVC.Data;

public class BookLendingDbContext : DbContext
{
    public BookLendingDbContext(DbContextOptions<BookLendingDbContext> options) : base(options)
    {
    }

    public DbSet<LoansModel> Loans { get; set; }
}
