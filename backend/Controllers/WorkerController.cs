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
    public async Task<IActionResult> Register(Worker worker)
    {
        if (ModelState.IsValid)
        {
            _context.Add(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }
        return View(worker);
    }

    public IActionResult Dashboard()
    {
        ViewBag.UserName = "Worker";
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string phone, string password)
    {
        var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Phone == phone);
        if (worker != null)
        {
            ViewBag.UserName = worker.FullName;
            return RedirectToAction(nameof(Dashboard));
        }
        ViewBag.Error = "Invalid phone or password";
        return View();
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Index", "Home");
    }
}
