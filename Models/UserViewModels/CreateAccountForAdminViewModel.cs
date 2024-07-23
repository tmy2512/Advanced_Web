using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserModel;
using System.ComponentModel.DataAnnotations;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels
{
    public class CreateAccountForAdminViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public ERole Role { get; set; }
        [Required]
        [MinLength(8)] // Adjust min length as needed
        public string Password { get; set; }
    }
}
