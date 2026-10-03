using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Appointments
        public async Task<IActionResult> Index()
        {
            // Include Doctor details so we can display Doctor Name & Specialization
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .OrderByDescending(a => a.Date)
                .ThenBy(a => a.Time)
                .ToListAsync();

            return View(appointments);
        }

        // GET: /Appointments/Create?doctorId=1
        public async Task<IActionResult> Create(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            // Pre-fill Doctor details for display on the form
            ViewBag.DoctorName = doctor.Name;
            ViewBag.Specialization = doctor.Specialization;

            var model = new Appointment
            {
                DoctorId = doctorId,
                Date = DateTime.Today // Default date to today
            };

            return View(model);
        }

        // POST: /Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            // Prevent double booking for the same Doctor, Date, and Time
            bool isDoubleBooked = await _context.Appointments.AnyAsync(a =>
                a.DoctorId == appointment.DoctorId &&
                a.Date.Date == appointment.Date.Date &&
                a.Time == appointment.Time);

            if (isDoubleBooked)
            {
                ModelState.AddModelError("Time", "This doctor already has an appointment booked at this exact date and time.");
            }

            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                // Redirect to Appointments Page after successful booking
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, re-populate doctor details for the view
            var doctor = await _context.Doctors.FindAsync(appointment.DoctorId);
            if (doctor != null)
            {
                ViewBag.DoctorName = doctor.Name;
                ViewBag.Specialization = doctor.Specialization;
            }

            return View(appointment);
        }
    }
}
