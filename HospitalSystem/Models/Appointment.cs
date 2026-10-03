using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalSystem.Attributes;
namespace HospitalSystem.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Patient name is required.")]
        [StringLength(100)]
        public string PatientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a date.")]
        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please select a time.")]
        [DataType(DataType.Time)]
        [ValidTimeSlot] // Added time slot validation
        public TimeSpan Time { get; set; }

        // Foreign Key
        [Required]
        public int DoctorId { get; set; }

        // Navigation Property
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }
    }
}
