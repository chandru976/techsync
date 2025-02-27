using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public async Task<IActionResult> AdminDashboard()
{
    var users = await _context.Users.ToListAsync();
    var subjects = await _context.Subjects.Include(s => s.AssignedStaff).ToListAsync();
    var courses = await _context.Courses.ToListAsync();
    
    ViewBag.Users = users;
    ViewBag.Subjects = subjects;
    ViewBag.Courses = courses;
    
    return View();
}
modelBuilder.Entity<User>().HasData(
    new User { 
        UserID = 1, 
        Email = "admin@techsync.com", 
        // ... rest of fields ...
    },
    // Add emails for other seed users
);

[AllowAnonymous]
public class RegistrationController : Controller
{
    private readonly TechSyncDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly EmailService _emailService;

    public RegistrationController(TechSyncDbContext context, IPasswordHasher<User> passwordHasher, EmailService emailService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        if (ModelState.IsValid)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await _emailService.SendEmailAsync(user.Email, "Verify your email", "Please verify your email.");
            return RedirectToAction("VerifyEmail", "Login");
        }
        return View(user);
    }
}