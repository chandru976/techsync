[Authorize(Roles = "Staff")]
public async Task<IActionResult> StaffDashboard()
{
    var staffId = int.Parse(HttpContext.Session.GetString("UserID"));
    var subjects = await _context.Subjects
        .Where(s => s.AssignedStaffID == staffId)
        .ToListAsync();
    
    var recentMarks = await _context.Marks
        .Include(m => m.Student)
        .Include(m => m.Subject)
        .Where(m => m.Subject.AssignedStaffID == staffId)
        .OrderByDescending(m => m.MarkID)
        .Take(5)
        .ToListAsync();
    
    ViewBag.Subjects = subjects;
    ViewBag.RecentMarks = recentMarks;
    
    return View();
}