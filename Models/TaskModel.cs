using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models
{
    [Table("Tasks")]
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
        public int ID { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "VerifyKey must be exactly 10 characters long.")]
        [RegularExpression(@"^[0-9].*", ErrorMessage = "VerifyKey must start with a number.")]
        public string VerifyKey
        {
            get; set;
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public DateTime UpdatedAt { get; set; }

        public EStatus Status { get; set; }

        // Navigation properties
        public ProjectModel Project { get; set; }
        public UserModel User { get; set; }
        public TaskModel()
        {
            UpdatedAt = DateTime.Now;
        }
        public enum EStatus
        {
            ToDo,
            Doing,
            Done
        }
    }
}
