using System.ComponentModel.DataAnnotations;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email là bắt buộc!")]
        [EmailAddress(ErrorMessage = "Kiem tra lai dinh dang email!")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password là bắt buộc!")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự!")] // Adjust min length as needed
        public string Password { get; set; }
    }
}
