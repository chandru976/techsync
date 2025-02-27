using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class StudentController : Controller
{
    public IActionResult Index() => View();
}