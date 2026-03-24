using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Karigaar360.Data;
using Karigaar360.Models;

namespace Karigaar360.Controllers;

public class JobController : Controller
{
    private readonly ILogger<JobController> _logger;
    private readonly ApplicationDbContext _context;
    private const decimal MINIMUM_WAGE_PER_HOUR = 400m;

    public JobController(ILogger<JobController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Create()
    {
        // Check if customer is logged in
        var customerId = HttpContext.Session.GetInt32("CustomerId");
        if (customerId == null)
        {
            return RedirectToAction("Login", "Customer");
        }

        var customer = _context.Customers.Find(customerId);
        if (customer == null)
        {
            return RedirectToAction("Login", "Customer");
        }

        var job = new Job
        {
            CustomerId = (int)customerId,
            CustomerName = customer.FullName,
            Location = customer.Address ?? ""
        };

        return View(job);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Job job)
    {
        var customerId = HttpContext.Session.GetInt32("CustomerId");
        if (customerId == null)
        {
            return RedirectToAction("Login", "Customer");
        }

        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
        {
            return RedirectToAction("Login", "Customer");
        }

        // Set customer info BEFORE validation
        job.CustomerId = (int)customerId;
        job.CustomerName = customer.FullName;
        if (string.IsNullOrEmpty(job.Location))
        {
            job.Location = customer.Address ?? "";
        }

        // Calculate fair price BEFORE validation
        job.FairPrice = MINIMUM_WAGE_PER_HOUR * job.EstimatedHours;

        // Validate that offered price is at least the fair price (minimum wage)
        if (job.OfferedPrice < job.FairPrice)
        {
            ModelState.AddModelError("OfferedPrice", 
                $"Please offer at least the minimum fair wage of Rs. {job.FairPrice:F2}");
        }

        if (ModelState.IsValid)
        {
            job.CreatedAt = DateTime.UtcNow;
            _context.Add(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyJobs));
        }

        return View(job);
    }

    public async Task<IActionResult> MyJobs()
    {
        var customerId = HttpContext.Session.GetInt32("CustomerId");
        if (customerId == null)
        {
            return RedirectToAction("Login", "Customer");
        }

        var jobs = await _context.Jobs
            .Where(j => j.CustomerId == customerId)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();

        return View(jobs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var job = await _context.Jobs
            .Include(j => j.Customer)
            .FirstOrDefaultAsync(j => j.Id == id);

        if (job == null)
        {
            return NotFound();
        }

        return View(job);
    }
}
