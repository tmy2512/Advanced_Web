using System.ComponentModel.DataAnnotations;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "Due date must be a future date";
        }
        public override bool IsValid(object value)
        {
            if(value is  DateTime)
            {
                DateTime dateValue = (DateTime)value;
                return dateValue > DateTime.Now;
            }
            return false;
        }
    }
}
