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
            .Include(j => j.Worker)
            .FirstOrDefaultAsync(j => j.Id == id);

        if (job == null)
        {
            return NotFound();
        }

        return View(job);
    }

    public async Task<IActionResult> Browse()
    {
        var workerId = HttpContext.Session.GetInt32("WorkerId");
        if (workerId == null)
        {
            return RedirectToAction("Login", "Worker");
        }

        // Get jobs that are open
        var jobs = await _context.Jobs
            .Where(j => j.Status == "Open")
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();

        return View(jobs);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApplyForJob(int id)
    {
        var workerId = HttpContext.Session.GetInt32("WorkerId");
        if (workerId == null)
        {
            return RedirectToAction("Login", "Worker");
        }

        // Check if already applied
        var alreadyApplied = await _context.JobApplications
            .AnyAsync(a => a.JobId == id && a.WorkerId == workerId);
        
        if (alreadyApplied)
        {
            TempData["Error"] = "You have already applied for this job.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var job = await _context.Jobs.FindAsync(id);
        if (job == null || job.Status != "Open")
        {
            return NotFound();
        }

        var application = new JobApplication
        {
            JobId = id,
            WorkerId = (int)workerId,
            Status = "Pending"
        };

        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Application submitted successfully!";
        return RedirectToAction(nameof(Browse));
    }

    public async Task<IActionResult> MyAcceptedJobs()
    {
        var workerId = HttpContext.Session.GetInt32("WorkerId");
        if (workerId == null)
        {
            return RedirectToAction("Login", "Worker");
        }

        var jobs = await _context.Jobs
            .Where(j => j.WorkerId == workerId)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();

        return View(jobs);
    }

    public async Task<IActionResult> MyApplications()
    {
        var workerId = HttpContext.Session.GetInt32("WorkerId");
        if (workerId == null)
        {
            return RedirectToAction("Login", "Worker");
        }

        var applications = await _context.JobApplications
            .Include(a => a.Job)
            .Where(a => a.WorkerId == workerId)
            .OrderByDescending(a => a.AppliedAt)
            .ToListAsync();

        return View(applications);
    }

    [HttpPost]
    public async Task<IActionResult> CompleteJob(int id)
    {
        var workerId = HttpContext.Session.GetInt32("WorkerId");
        if (workerId == null)
        {
            return RedirectToAction("Login", "Worker");
        }

        var job = await _context.Jobs.FindAsync(id);
        if (job == null || job.WorkerId != workerId)
        {
            return NotFound();
        }

        job.Status = "Completed";
        job.CompletedAt = DateTime.UtcNow;

        // Update worker stats
        var worker = await _context.Workers.FindAsync(workerId);
        if (worker != null)
        {
            worker.TotalJobsCompleted += 1;
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(MyAcceptedJobs));
    }

    public async Task<IActionResult> ViewApplicants(int jobId)
    {
        var customerId = HttpContext.Session.GetInt32("CustomerId");
        if (customerId == null)
        {
            return RedirectToAction("Login", "Customer");
        }

        var job = await _context.Jobs
            .Include(j => j.Customer)
            .FirstOrDefaultAsync(j => j.Id == jobId && j.CustomerId == customerId);

        if (job == null)
        {
            return NotFound();
        }

        var applicants = await _context.JobApplications
            .Include(a => a.Worker)
            .Where(a => a.JobId == jobId)
            .ToListAsync();

        ViewBag.JobTitle = job!.Title;
        ViewBag.JobId = job.Id;

        return View(applicants);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> HireWorker(int applicationId)
    {
        var customerId = HttpContext.Session.GetInt32("CustomerId");
        if (customerId == null)
        {
            return RedirectToAction("Login", "Customer");
        }

        var application = await _context.JobApplications
            .Include(a => a.Job)
            .Include(a => a.Worker)
            .FirstOrDefaultAsync(a => a.Id == applicationId && a.Job.CustomerId == customerId);

        if (application == null || application.Job == null || application.Job.Status != "Open")
        {
            return NotFound();
        }

        if (application.Worker == null)
        {
            return NotFound();
        }

        // 1. Accept the selected worker
        application.Status = "Accepted";
        
        // 2. Reject all other pending applicants for this job
        var otherApplicants = await _context.JobApplications
            .Where(a => a.JobId == application.JobId && a.Id != applicationId && a.Status == "Pending")
            .ToListAsync();
        
        foreach (var other in otherApplicants)
        {
            other.Status = "Rejected";
        }

        // 3. Update job status and assign worker
        application.Job.Status = "InProgress";
        application.Job.WorkerId = application.WorkerId;
        application.Job.WorkerName = application.Worker!.FullName;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(MyJobs));
    }
}
