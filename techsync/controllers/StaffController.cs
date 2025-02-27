using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class StaffController : Controller
{
    public IActionResult Index() => View();
}