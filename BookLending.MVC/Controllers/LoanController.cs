using BookLending.MVC.Data;
using BookLending.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLending.MVC.Controllers;

public class LoanController : Controller
{
    private readonly BookLendingDbContext _bookLendingDb;

    public LoanController(BookLendingDbContext bookLendingDb)
    {
        _bookLendingDb = bookLendingDb;
    }

    public IActionResult Index()
    {
        IEnumerable<LoansModel> loans = _bookLendingDb.Loans;

        return View(loans);
    }
}
