using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

[Authorize]
public class UserController : Controller
{
    private readonly TechSyncDbContext _context;

    public UserController(TechSyncDbContext context)
    {
        _context = context;
    }

    public IActionResult EditProfile() => View();

    [HttpPost]
    public async Task<IActionResult> EditProfile(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile");
        }
        return View(user);
    }
}