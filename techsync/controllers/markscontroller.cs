using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class MarksController : Controller
{
    public IActionResult EnterMarks() => View();
}