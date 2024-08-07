using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels
{
	public class UserDTOModel
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
		public int UserID { get; set; }

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
	}
}
