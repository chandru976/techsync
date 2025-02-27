using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class AdminController : Controller
{
    public IActionResult Index() => View();
}