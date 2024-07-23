using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskModel;
using System.ComponentModel.DataAnnotations;
using ManagementAssistanceForBusinessWeb_OnlyRole.Validation;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels
{
    public class CreateNewTaskModel
    {
        [Required(ErrorMessage = "Tên task là bắt buộc")]
        [MaxLength(100)]
        public string Name { get; set; }

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
