using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly TechSyncDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public LoginController(TechSyncDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public IActionResult VerifyEmail() => View();

    [HttpPost]
    public async Task<IActionResult> VerifyEmail(string email, string token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.EmailVerificationToken == token);
        if (user != null)
        {
            user.EmailVerified = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }
        ModelState.AddModelError("", "Invalid email or token.");
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
