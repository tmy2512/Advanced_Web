using System.ComponentModel.DataAnnotations;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models.ProjectViewModels
{
    public class ProjectDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Tên dự án là bắt buộc")]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } 
    }
}
