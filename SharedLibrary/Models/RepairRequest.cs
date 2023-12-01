using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class RepairRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string RepairType { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public DateTime Timestamp { get; set; }

        public RepairRequest()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
