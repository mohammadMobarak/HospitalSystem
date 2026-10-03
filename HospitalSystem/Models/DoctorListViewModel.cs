namespace HospitalSystem.Models
{
    public class DoctorListViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; } = new List<Doctor>();

        // Filter inputs
        public string? SearchName { get; set; }
        public string? SelectedSpecialization { get; set; }

        // Dropdown options
        public List<string> Specializations { get; set; } = new List<string>();

        // Pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
