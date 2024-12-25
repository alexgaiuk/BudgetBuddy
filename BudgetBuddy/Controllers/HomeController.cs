using System.Diagnostics;
using BudgetBuddy.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BudgetBuddyDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, BudgetBuddyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _dbContext.Expenses.ToList();

            var totalExpenses = allExpenses.Sum(expense => expense.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                // edition -> load an expense by id.
                var expenseInDb = _dbContext.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseInDb);
            }

            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            // Go to the database and find the first element that matches the id parameter.
            var expenseInDb = _dbContext.Expenses.SingleOrDefault(expense => expense.Id == id);
            _dbContext.Expenses.Remove(expenseInDb);
            _dbContext.SaveChanges();
            return RedirectToAction("Expenses");
        }
        
        // Redirect after submiting the form.
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                // Creating an expense.
                _dbContext.Expenses.Add(model);

            }
            else
            {
                // Editing an expense.
                _dbContext.Expenses.Update(model);
            }

            _dbContext.SaveChanges();

            // Call the Index() method and redirect us to View page.
            return RedirectToAction("Expenses");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
