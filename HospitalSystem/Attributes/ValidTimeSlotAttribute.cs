using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Attributes
{
    public class ValidTimeSlotAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is TimeSpan time)
            {
                var startWorkingTime = new TimeSpan(9, 0, 0);   // 09:00 AM
                var endWorkingTime = new TimeSpan(16, 30, 0);  // 04:30 PM

                // Rule 1: Must be between 09:00 AM and 04:30 PM
                if (time < startWorkingTime || time > endWorkingTime)
                {
                    return new ValidationResult("Appointments must be booked between 09:00 AM and 04:30 PM.");
                }

                // Rule 2: Must be on a 30-minute boundary (e.g., :00 or :30)
                if (time.Minutes % 30 != 0 || time.Seconds != 0)
                {
                    return new ValidationResult("Appointment time must be in 30-minute intervals (e.g., 09:00, 09:30).");
                }
            }

            return ValidationResult.Success;
        }
    }
}
