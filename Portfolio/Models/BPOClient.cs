using System;
using System.ComponentModel.DataAnnotations;

namespace BPOSolution.Models
{
    public class BPOClient
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string CompanyName { get; set; }
        public string Message { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int IsSent { get; set; }
    }
}
