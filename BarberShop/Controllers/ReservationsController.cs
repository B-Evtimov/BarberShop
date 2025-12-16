using BarberShop.Data;
using BarberShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ReservationsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReservationsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
        var reservations = await _db.Reservations.Include(r => r.Barber).ToListAsync();

        ViewBag.StartOfWeek = startOfWeek;
        ViewBag.Barbers = await _db.Barbers.ToListAsync();

        return View(reservations);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DateTime date, int barberId, ServiceType service)
    {
        var user = await _userManager.GetUserAsync(User);

        bool taken = _db.Reservations.Any(r =>
            r.BarberId == barberId &&
            r.Date == date
        );

        if (taken)
        {
            TempData["Error"] = "Този час вече е зает.";
            return RedirectToAction("Index");
        }

        var reservation = new Reservation
        {
            UserId = user.Id,
            BarberId = barberId,
            Date = date,
            Service = service,
            Price = (int)service
        };

        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Резервацията е направена успешно!";
        return RedirectToAction("Index");
    }
}