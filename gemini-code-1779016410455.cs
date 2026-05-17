using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KibabiiRevisionGroup.Data;
using KibabiiRevisionGroup.Models;
// Using BCrypt.Net-Next for easy, secure password hashing
using BCrypt.Net; 

namespace KibabiiRevisionGroup.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Student student, string password)
        {
            if (await _context.Students.AnyAsync(s => s.RegNumber == student.RegNumber))
            {
                ModelState.AddModelError("RegNumber", "This Registration Number is already registered.");
            }

            if (ModelState.IsValid)
            {
                // Hash the password securely before saving
                student.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                
                _context.Add(student);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Login");
            }
            return View(student);
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string regNumber, string password)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.RegNumber == regNumber);
            
            if (student != null && BCrypt.Net.BCrypt.Verify(password, student.PasswordHash))
            {
                // Simple Session-based authentication for tracking state
                HttpContext.Session.SetString("UserRegNum", student.RegNumber);
                HttpContext.Session.SetString("UserName", student.FullName);
                
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Invalid Registration Number or Password.");
            return View();
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}