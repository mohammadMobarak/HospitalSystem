using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                // Rule 1: Prevent past dates
                if (date.Date < DateTime.Today)
                {
                    return new ValidationResult("Appointment date cannot be in the past.");
                }

                // Rule 2: Allow Sunday (Sunday = DayOfWeek.Sunday) through Thursday (DayOfWeek.Thursday)
                // Friday and Saturday are weekend days
                if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    return new ValidationResult("Appointments are only available Sunday through Thursday.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
