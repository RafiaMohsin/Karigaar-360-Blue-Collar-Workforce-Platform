using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Karigaar360.Data;
using Karigaar360.Models;

namespace Karigaar360.Controllers;

public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ApplicationDbContext _context;

    public CustomerController(ILogger<CustomerController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Customer customer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }
        return View(customer);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string phone, string password)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Phone == phone);
        if (customer != null)
        {
            ViewBag.UserName = customer.FullName;
            return RedirectToAction(nameof(Dashboard));
        }
        ViewBag.Error = "Invalid phone or password";
        return View();
    }

    public IActionResult Dashboard()
    {
        ViewBag.UserName = "Customer";
        return View();
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Index", "Home");
    }
}
