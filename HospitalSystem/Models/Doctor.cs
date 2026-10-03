using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Specialization { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        // Navigation property for EF Core
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
