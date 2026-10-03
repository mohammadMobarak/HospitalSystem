using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 3; // 3 doctors per page

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Doctors
        public async Task<IActionResult> Index(string? searchName, string? specialization, int page = 1)
        {
            var query = _context.Doctors.AsQueryable();

            // 1. Filter by Name
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(d => d.Name.Contains(searchName));
            }

            // 2. Filter by Specialization
            if (!string.IsNullOrWhiteSpace(specialization))
            {
                query = query.Where(d => d.Specialization == specialization);
            }

            // 3. Pagination calculations
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            page = Math.Clamp(page, 1, Math.Max(1, totalPages));

            // 4. Fetch paged results
            var doctors = await query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            // 5. Fetch distinct specializations for dropdown filter
            var specializations = await _context.Doctors
                .Select(d => d.Specialization)
                .Distinct()
                .ToListAsync();

            var viewModel = new DoctorListViewModel
            {
                Doctors = doctors,
                SearchName = searchName,
                SelectedSpecialization = specialization,
                Specializations = specializations,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }
    }
}
