using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Karigaar360.Data;
using Karigaar360.Models;

namespace Karigaar360.Controllers;

public class WorkerController : Controller
{
    private readonly ILogger<WorkerController> _logger;
    private readonly ApplicationDbContext _context;

    public WorkerController(ILogger<WorkerController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([Bind("FullName,Phone,Profession,ExperienceYears,Location,PasswordHash")] Worker worker)
    {
        if (ModelState.IsValid)
        {
            // Check if phone number is already registered
            var existingWorker = await _context.Workers.AnyAsync(w => w.Phone == worker.Phone);
            if (existingWorker)
            {
                ModelState.AddModelError("Phone", "This phone number is already registered.");
                return View(worker);
            }

            try 
            {
                // Hash the password before saving
                worker.PasswordHash = BCrypt.Net.BCrypt.HashPassword(worker.PasswordHash);
                
                // Set default values for new workers
                worker.Rating = 0.0;
                worker.TotalJobsCompleted = 0;
                worker.TotalEarnings = 0.0;
                worker.IsAvailable = true;

                _context.Add(worker);
                await _context.SaveChangesAsync();
                
                // Set session after successful registration
                HttpContext.Session.SetInt32("WorkerId", worker.Id);
                HttpContext.Session.SetString("WorkerName", worker.FullName);
                
                return RedirectToAction(nameof(Dashboard));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during worker registration");
                ModelState.AddModelError("", "An error occurred during registration. Please try again.");
            }
        }
        return View(worker);
    }

    public async Task<IActionResult> Dashboard()
    {
        var workerId = HttpContext.Session.GetInt32("WorkerId");
        if (workerId == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var worker = await _context.Workers.FindAsync(workerId);
        if (worker == null)
        {
            return RedirectToAction(nameof(Login));
        }

        ViewBag.UserName = worker.FullName;
        ViewBag.Rating = worker.Rating;
        ViewBag.JobsCompleted = worker.TotalJobsCompleted;
        ViewBag.TotalEarnings = worker.TotalEarnings;

        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string phone, string password)
    {
        var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Phone == phone);
        if (worker != null && BCrypt.Net.BCrypt.Verify(password, worker.PasswordHash))
        {
            // Set session after successful login
            HttpContext.Session.SetInt32("WorkerId", worker.Id);
            HttpContext.Session.SetString("WorkerName", worker.FullName);
            
            ViewBag.UserName = worker.FullName;
            return RedirectToAction(nameof(Dashboard));
        }
        ViewBag.Error = "Invalid phone or password";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
