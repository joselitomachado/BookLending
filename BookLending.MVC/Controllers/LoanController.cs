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

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<LoansModel> loans = _bookLendingDb.Loans;

        return View(loans);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if(id == null || id == 0)
        {
            return NotFound();
        }

        LoansModel loan = _bookLendingDb.Loans.FirstOrDefault(x => x.Id == id);

        if(loan == null)
        {
            return NotFound();
        }

        return View(loan);
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        LoansModel loan = _bookLendingDb.Loans.FirstOrDefault(x => x.Id == id);

        if (loan == null)
        {
            return NotFound();
        }

        return View(loan);
    }

    [HttpPost]
    public IActionResult Register(LoansModel loans)
    {
        if (ModelState.IsValid)
        {
            _bookLendingDb.Loans.Add(loans);
            _bookLendingDb.SaveChanges();

            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpPost]
    public IActionResult Edit(LoansModel loan)
    {
        if (ModelState.IsValid)
        {
            _bookLendingDb.Loans.Update(loan);
            _bookLendingDb.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(loan);
    }

    [HttpPost]
    public IActionResult Delete(LoansModel loan)
    {
        if(loan == null)
        {
            return NotFound();
        }

        _bookLendingDb.Loans.Remove(loan);
        _bookLendingDb.SaveChanges();

        return RedirectToAction("Index");
    }

}
