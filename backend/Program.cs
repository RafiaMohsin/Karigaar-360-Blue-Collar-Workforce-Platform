using Microsoft.EntityFrameworkCore;
using Karigaar360.Data;
using Microsoft.AspNetCore.ResponseCompression; // NFR 1: Performance
using Microsoft.AspNetCore.RateLimiting;      // NFR 2: Security
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// NFR 1: Performance - Response Compression (Makes the website load faster)
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

// NFR 2: Security - Rate Limiting (Prevents DDoS and brute-force attacks)
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100, // Max 100 requests
                QueueLimit = 2,
                Window = TimeSpan.FromMinutes(1) // Per minute per IP
            }));
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Add DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session support
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Apply NFR 1 (Performance)
app.UseResponseCompression();

// Apply NFR 2 (Security)
app.UseRateLimiter();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();