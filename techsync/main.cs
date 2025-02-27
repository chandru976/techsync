[Authorize(Roles = "Staff")]
public class MarksController : Controller
{
    private readonly TechSyncDbContext _context;

    public MarksController(TechSyncDbContext context) => _context = context;

    public IActionResult EnterMarks()
    {
        var staffId = int.Parse(HttpContext.Session.GetString("UserID"));
        var subjects = _context.Subjects.Where(s => s.AssignedStaffID == staffId).ToList();
        var students = _context.Users.Where(u => u.Role == "Student" && u.Department == "CS").ToList(); // Filter by department
        ViewBag.Subjects = subjects;
        ViewBag.Students = students;
        return View();
    }

    [HttpPost]
    public IActionResult EnterMarks(Marks mark)
    {
        if (ModelState.IsValid)
        {
            _context.Marks.Add(mark);
            _context.SaveChanges();
            return RedirectToAction("StaffDashboard", "Dashboard");
        }
        return View();
    }
}
[Authorize(Roles = "Student")]
public class FeedbackController : Controller
{
    private readonly TechSyncDbContext _context;

    public FeedbackController(TechSyncDbContext context) => _context = context;

    public IActionResult SubmitFeedback() => View();

    [HttpPost]
    public IActionResult SubmitFeedback(Feedback feedback)
    {
        feedback.StudentID = int.Parse(HttpContext.Session.GetString("UserID"));
        feedback.Status = "Pending";
        _context.Feedbacks.Add(feedback);
        _context.SaveChanges();
        return RedirectToAction("StudentDashboard", "Dashboard");
    }
}
[Authorize(Roles = "Admin")]
public class SubjectsController : Controller
{
    private readonly TechSyncDbContext _context;

    public SubjectsController(TechSyncDbContext context) => _context = context;

    public IActionResult Index()
    {
        var subjects = _context.Subjects.Include(s => s.AssignedStaff).ToList();
        return View(subjects);
    }

    public IActionResult Edit(int id)
    {
        var subject = _context.Subjects.Find(id);
        ViewBag.Staff = _context.Users.Where(u => u.Role == "Staff").ToList();
        return View(subject);
    }

    [HttpPost]
    public IActionResult Edit(Subject updatedSubject)
    {
        _context.Update(updatedSubject);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
[Authorize(Roles = "Admin")]
public class AdminController : Controller { /* ... */ }

[Authorize(Roles = "Staff")]
public class StaffController : Controller { /* ... */ }
[Authorize(Roles = "Student")]
public async Task<IActionResult> StudentDashboard()
{
    var studentId = int.Parse(HttpContext.Session.GetString("UserID"));
    var marks = await _context.Marks
        .Include(m => m.Subject)
        .Where(m => m.StudentID == studentId)
        .ToListAsync();
    
    var feedback = await _context.Feedbacks
        .Where(f => f.StudentID == studentId)
        .OrderByDescending(f => f.FeedbackID)
        .Take(5)
        .ToListAsync();
    
    ViewBag.Marks = marks;
    ViewBag.Feedback = feedback;
    
    return View();
}
// Inside LoginController.Authenticate()
if (user != null && passwordVerification == PasswordVerificationResult.Success)
{
    HttpContext.Session.SetString("UserID", user.UserID.ToString()); // Add this line
    HttpContext.Session.SetString("Username", user.Username);
    HttpContext.Session.SetString("Role", user.Role);
    // ... rest of the code ...
}
public class User
{
    [Key]
    public int UserID { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } // New field

    public bool IsEmailVerified { get; set; } = false; // New field
    public string? VerificationOTP { get; set; } // New field
    public DateTime? OTPExpiry { get; set; } // New field

    // ... rest of existing fields ...
}