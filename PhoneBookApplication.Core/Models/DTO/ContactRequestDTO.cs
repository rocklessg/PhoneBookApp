using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApplication.Core.Models.DTO
{
    public class ContactRequestDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name should not be more than 50 characters long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Phone number should not be more than 20 characters long")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}
