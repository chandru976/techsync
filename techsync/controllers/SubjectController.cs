using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class SubjectsController : Controller
{
    public IActionResult Edit() => View();
}