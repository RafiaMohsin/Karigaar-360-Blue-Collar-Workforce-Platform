using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Karigaar360.Data;
using Karigaar360.Models;

namespace Karigaar360.Controllers;

public class ChatController : Controller
{
    private readonly ApplicationDbContext _context;

    public ChatController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int receiverId, bool isReceiverWorker)
    {
        var workerId = HttpContext.Session.GetInt32("WorkerId");
        var customerId = HttpContext.Session.GetInt32("CustomerId");

        if (workerId == null && customerId == null)
        {
            return RedirectToAction("Login", "Customer");
        }

        int currentUserId = workerId ?? customerId ?? 0;
        bool isCurrentUserWorker = workerId != null;

        // Fetch user names for the UI header
        string receiverName = "";
        if (isReceiverWorker)
        {
            var worker = await _context.Workers.FindAsync(receiverId);
            receiverName = worker?.FullName ?? "Worker";
        }
        else
        {
            var customer = await _context.Customers.FindAsync(receiverId);
            receiverName = customer?.FullName ?? "Customer";
        }

        // Load conversation history
        // A message belongs to this conversation if:
        // (Sender=CurrentUser AND Receiver=OtherUser AND SenderType=CurrentUserType)
        // OR (Sender=OtherUser AND Receiver=CurrentUser AND SenderType=OtherUserType)
        
        var messages = await _context.ChatMessages
            .Where(m => 
                (m.SenderId == currentUserId && m.ReceiverId == receiverId && m.IsWorkerSender == isCurrentUserWorker) ||
                (m.SenderId == receiverId && m.ReceiverId == currentUserId && m.IsWorkerSender == isReceiverWorker)
            )
            .OrderBy(m => m.Timestamp)
            .ToListAsync();

        ViewBag.CurrentUserId = currentUserId;
        ViewBag.IsCurrentUserWorker = isCurrentUserWorker;
        ViewBag.ReceiverId = receiverId;
        ViewBag.IsReceiverWorker = isReceiverWorker;
        ViewBag.ReceiverName = receiverName;

        return View(messages);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(int receiverId, bool isReceiverWorker, string content)
    {
        if (string.IsNullOrWhiteSpace(content)) return BadRequest();

        var workerId = HttpContext.Session.GetInt32("WorkerId");
        var customerId = HttpContext.Session.GetInt32("CustomerId");

        if (workerId == null && customerId == null) return Unauthorized();

        var message = new ChatMessage
        {
            SenderId = workerId ?? customerId ?? 0,
            ReceiverId = receiverId,
            Content = content,
            Timestamp = DateTime.UtcNow,
            IsWorkerSender = workerId != null
        };

        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { receiverId, isReceiverWorker });
    }
}
