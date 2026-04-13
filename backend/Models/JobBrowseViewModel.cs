using System.Collections.Generic;

namespace Karigaar360.Models
{
    public class JobBrowseViewModel
    {
        public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
        public HashSet<int> AppliedJobIds { get; set; } = new HashSet<int>();
        public string? SearchTerm { get; set; }
        public string WorkerProfession { get; set; } = string.Empty;
    }
}
