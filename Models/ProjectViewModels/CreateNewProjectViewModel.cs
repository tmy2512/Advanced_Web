using System.ComponentModel.DataAnnotations;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models.ViewModels
{
    public class CreateNewProjectViewModel
    {
        [Required(ErrorMessage = "Tên project là bắt buộc")]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; } 

        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.Now;

    }
}
