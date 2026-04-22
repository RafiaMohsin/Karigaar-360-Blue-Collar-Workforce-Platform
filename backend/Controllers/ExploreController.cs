using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Karigaar360.Data;
using Karigaar360.Models;

namespace Karigaar360.Controllers;

public class ExploreController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExploreController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Search(string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return RedirectToAction("Index", "Home");
        }

        q = q.Trim().ToLower();

        var workers = await _context.Workers
            .Where(w => w.FullName.ToLower().Contains(q) || w.Profession.ToLower().Contains(q))
            .ToListAsync();

        var customers = await _context.Customers
            .Where(c => c.FullName.ToLower().Contains(q))
            .ToListAsync();

        ViewBag.Query = q;
        
        var viewModel = new SearchResultsViewModel
        {
            Workers = workers,
            Customers = customers
        };

        return View(viewModel);
    }
}

public class SearchResultsViewModel
{
    public List<Worker> Workers { get; set; } = new List<Worker>();
    public List<Customer> Customers { get; set; } = new List<Customer>();
}
