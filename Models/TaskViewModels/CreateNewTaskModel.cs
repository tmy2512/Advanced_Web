using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskModel;
using System.ComponentModel.DataAnnotations;
using ManagementAssistanceForBusinessWeb_OnlyRole.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels
{
    public class CreateNewTaskModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
        public int ID { get; set; }
        [Required(ErrorMessage = "Tên task là bắt buộc")]
        [MaxLength(100)]
        public string Name { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "VerifyKey must be exactly 10 characters long.")]
        [RegularExpression(@"^[0-9].*", ErrorMessage = "VerifyKey must start with a number.")]
        public string VerifyKey
        {
            get; set;
        }

        public string Content { get; set; }

        [DataType(DataType.Date)]
        [FutureDate]
        [Required(ErrorMessage = "Ngày đến hạn là bắt buộc")]
        public DateTime DueDate { get; set; }

        public int ProjectID { get; set; }

        public int UserID { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public EStatus Status { get; set; }
    }
}
