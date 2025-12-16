using BarberShop.Data;
using BarberShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    // ✅ Всички резервации
    public async Task<IActionResult> Reservations()
    {
        var reservations = await _db.Reservations
            .Include(r => r.User)
            .Include(r => r.Barber)
            .OrderBy(r => r.Date)
            .ToListAsync();

        return View(reservations);
    }

    // ✅ Изтриване на резервация
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var res = await _db.Reservations.FindAsync(id);
        if (res != null)
        {
            _db.Reservations.Remove(res);
            await _db.SaveChangesAsync();
        }

        return RedirectToAction("Reservations");
    }

    // ✅ Всички потребители
    public async Task<IActionResult> Users()
    {
        var users = await _db.Users.ToListAsync();
        return View(users);
    }

    // ✅ Детайли за потребител
    public async Task<IActionResult> UserDetails(string id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound();

        var reservations = await _db.Reservations
            .Where(r => r.UserId == id)
            .Include(r => r.Barber)
            .OrderBy(r => r.Date)
            .ToListAsync();

        ViewBag.Reservations = reservations;

        return View(user);
    }
}