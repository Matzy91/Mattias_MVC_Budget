using Microsoft.AspNetCore.Mvc;
using MyEconomy.Data;
using MyEconomy.Models;
using Microsoft.Extensions.Logging;

namespace MyEconomy.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
{
    var entries = _context.BudgetEntries.ToList();
    var totalIncome = entries.Where(e => e.Type == "Income").Sum(e => e.Amount);
    var totalExpense = -entries.Where(e => e.Type == "Expense").Sum(e => e.Amount);
    var balance = entries.Sum(e => e.Amount);

    var model = new BudgetViewModel
    {
        Entries = entries,
        TotalIncome = totalIncome,
        TotalExpense = totalExpense,
        Balance = balance
    };

    return View(model);
}


    public IActionResult Budget()
    {
        var entries = _context.BudgetEntries.ToList();
        var totalIncome = entries.Where(e => e.Type == "Income").Sum(e => e.Amount);
        var totalExpense = -entries.Where(e => e.Type == "Expense").Sum(e => e.Amount);
        var balance = entries.Sum(e => e.Amount);

        var budgetmodel = new BudgetViewModel
        {
            Entries = entries,
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            Balance = balance
        };

        return View(budgetmodel);
    }

    [HttpPost]
    public IActionResult AddBudgetEntry(BudgetEntry entry)
    {
        if (ModelState.IsValid)
        {
            if (entry.Type == "Expense" && entry.Amount > 0)
            {
                entry.Amount = -entry.Amount;
            }
            _context.BudgetEntries.Add(entry);
            _context.SaveChanges();
        }
        return RedirectToAction("Budget");
    }

    [HttpPost]
    public IActionResult UpdateBudgetEntry([FromBody] BudgetEntry entry)
    {
        if (ModelState.IsValid)
        {
            _context.BudgetEntries.Update(entry);
            _context.SaveChanges();
        }
        return Ok();
    }


    [HttpDelete]
    public IActionResult DeleteBudgetEntry(int id)
    {
        var entry = _context.BudgetEntries.Find(id);
        if (entry != null)
        {
            _context.BudgetEntries.Remove(entry);
            _context.SaveChanges();
        }
        return RedirectToAction("Budget");
    }
    
    [HttpPost]
    public IActionResult SubmitEntries([FromBody] List<BudgetEntry> entries)
    {
        foreach (var entry in entries)
        {
            if (entry.Type == "Expense" && entry.Amount > 0)
            {
                entry.Amount = -entry.Amount;
            }
            _context.BudgetEntries.Add(entry);
        }
        _context.SaveChanges();
        return Ok();
    }
}
