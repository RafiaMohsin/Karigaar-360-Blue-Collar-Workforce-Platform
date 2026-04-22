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
            // Hash the password before saving
            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);
            
            _context.Add(customer);
            await _context.SaveChangesAsync();
            
            // Set session after successful registration
            HttpContext.Session.SetInt32("CustomerId", customer.Id);
            HttpContext.Session.SetString("CustomerName", customer.FullName);
            
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
        if (customer != null && BCrypt.Net.BCrypt.Verify(password, customer.PasswordHash))
        {
            // Set session after successful login
            HttpContext.Session.SetInt32("CustomerId", customer.Id);
            HttpContext.Session.SetString("CustomerName", customer.FullName);
            
            ViewBag.UserName = customer.FullName;
            return RedirectToAction(nameof(Dashboard));
        }
        ViewBag.Error = "Invalid phone or password";
        return View();
    }

    public async Task<IActionResult> Dashboard()
    {
        var customerId = HttpContext.Session.GetInt32("CustomerId");
        if (customerId == null) return RedirectToAction(nameof(Login));

        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null) return RedirectToAction(nameof(Login));
        
        // Real stats
        ViewBag.TotalJobs = await _context.Jobs.CountAsync(j => j.CustomerId == customerId);
        ViewBag.CompletedJobs = await _context.Jobs.CountAsync(j => j.CustomerId == customerId && j.Status == "Completed");
        ViewBag.ActiveJobs = await _context.Jobs.CountAsync(j => j.CustomerId == customerId && j.Status == "InProgress");
        
        ViewBag.UserName = customer.FullName;
        return View();
    }

    public async Task<IActionResult> Profile(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();

        // Get job history for this customer (optional, but shows credibility)
        var jobs = await _context.Jobs
            .Where(j => j.CustomerId == id)
            .OrderByDescending(j => j.CreatedAt)
            .Take(5)
            .ToListAsync();

        ViewBag.RecentJobs = jobs;
        return View(customer);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
