using System.ComponentModel.DataAnnotations;

namespace Karigaar360.Models;

public class ChatMessage
{
    public int Id { get; set; }
    
    [Required]
    public int SenderId { get; set; }
    
    [Required]
    public int ReceiverId { get; set; }
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    // Flag to identify if sender is a Worker or Customer
    // True if Sender is Worker, False if Sender is Customer
    public bool IsWorkerSender { get; set; }
}
