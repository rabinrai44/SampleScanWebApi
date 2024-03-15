using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleScanWebApi.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int WorkflowId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}