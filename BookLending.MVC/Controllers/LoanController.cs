﻿using BookLending.MVC.Data;
using BookLending.MVC.Models;
using BookLending.MVC.Services.SessionService;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookLending.MVC.Controllers;

public class LoanController : Controller
{
    private readonly BookLendingDbContext _bookLendingDb;
    private readonly ISessionInterface _sessionInterface;

    public LoanController(BookLendingDbContext bookLendingDb, ISessionInterface sessionInterface)
    {
        _bookLendingDb = bookLendingDb;
        _sessionInterface = sessionInterface;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var user = _sessionInterface.GetSession();

        if(user == null)
        {
            return RedirectToAction("Login", "Login");
        }

        IEnumerable<LoansModel> loans = _bookLendingDb.Loans;

        return View(loans);
    }

    [HttpGet]
    public IActionResult Register()
    {
        var user = _sessionInterface.GetSession();

        if (user == null)
        {
            return RedirectToAction("Login", "Login");
        }

        return View();
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        var user = _sessionInterface.GetSession();

        if (user == null)
        {
            return RedirectToAction("Login", "Login");
        }

        if (id == null || id == 0)
        {
            return NotFound();
        }

        var loan = _bookLendingDb.Loans.FirstOrDefault(x => x.Id == id);

        if (loan == null)
        {
            return NotFound();
        }

        return View(loan);
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        var user = _sessionInterface.GetSession();

        if (user == null)
        {
            return RedirectToAction("Login", "Login");
        }

        if (id == null || id == 0)
        {
            return NotFound();
        }

        var loan = _bookLendingDb.Loans.FirstOrDefault(x => x.Id == id);

        if (loan == null)
        {
            return NotFound();
        }

        return View(loan);
    }

    public IActionResult Export()
    {
        var dados = GetData();

        using (XLWorkbook workBook = new())
        {
            workBook.AddWorksheet(dados, "Dados Empréstimos");

            using (MemoryStream ms = new())
            {
                workBook.SaveAs(ms);
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "emprestimo.xls");
            }
        }
    }

    private DataTable GetData()
    {
        DataTable dataTable = new();

        dataTable.TableName = "Dados empréstimos";
        dataTable.Columns.Add("Recebedor", typeof(string));
        dataTable.Columns.Add("Fornecedor", typeof(string));
        dataTable.Columns.Add("Livro", typeof(string));
        dataTable.Columns.Add("Data empréstimo", typeof(DateTime));

        var dados = _bookLendingDb.Loans.ToList();

        if (dados.Count > 0)
        {
            dados.ForEach(loan =>
            {
                dataTable.Rows.Add(loan.Recipient, loan.Provider, loan.BorrowedBook, loan.LoanDate);
            });
        }

        return dataTable;
    }

    [HttpPost]
    public IActionResult Register(LoansModel loan)
    {
        if (ModelState.IsValid)
        {
            loan.LoanDate = DateTime.Now;

            _bookLendingDb.Loans.Add(loan);
            _bookLendingDb.SaveChanges();

            TempData["SuccessMessage"] = "Cadastro realizado com sucesso.";

            return RedirectToAction("Index");
        }

        TempData["ErrorMessage"] = "Ocorreu um erro ao realizar um cadastro.";

        return View();
    }

    [HttpPost]
    public IActionResult Edit(LoansModel loan)
    {
        if (ModelState.IsValid)
        {
            var loanDb = _bookLendingDb.Loans.Find(loan.Id);

            loanDb.Recipient = loan.Recipient;
            loanDb.Provider = loan.Provider;
            loanDb.BorrowedBook = loan.BorrowedBook;

            _bookLendingDb.Loans.Update(loanDb);
            _bookLendingDb.SaveChanges();

            TempData["SuccessMessage"] = "Edição realizado com sucesso.";

            return RedirectToAction("Index");
        }

        TempData["ErrorMessage"] = "Ocorreu um erro ao realizar uma edição.";

        return View(loan);
    }

    [HttpPost]
    public IActionResult Delete(LoansModel loan)
    {
        if (loan == null)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro ao realizar uma remoção.";

            return NotFound();
        }

        _bookLendingDb.Loans.Remove(loan);
        _bookLendingDb.SaveChanges();

        TempData["SuccessMessage"] = "Remoção realizada com sucesso.";

        return RedirectToAction("Index");
    }
}
