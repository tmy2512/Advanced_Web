using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.UserFolder;
using System.ComponentModel.DataAnnotations;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Validation
{
	public class UniqueUsernameAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				var _userRepository = validationContext.GetService<IUserRepository>();
				string username = value.ToString();
				//bool isUnique = _userRepository.IsUsernameUnique(username);

				//if (!isUnique)
				//{
				//	return new ValidationResult("Username đã tồn tại.");
				//}
			}

			return ValidationResult.Success;
		}
	}
}
