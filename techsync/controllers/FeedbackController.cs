using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class FeedbackController : Controller
{
    public IActionResult SubmitFeedback() => View();
}