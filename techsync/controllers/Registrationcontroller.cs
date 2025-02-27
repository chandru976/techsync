using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

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