using BarberShop.Data;
using BarberShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class VerifyCodeModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public VerifyCodeModel(ApplicationDbContext db, SignInManager<ApplicationUser> signInManager)
    {
        _db = db;
        _signInManager = signInManager;
    }

    [BindProperty]
    public string Code { get; set; }

    public string UserId { get; set; }
    public string ReturnUrl { get; set; }

    public void OnGet(string userId, string returnUrl)
    {
        UserId = userId;
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string userId, string returnUrl)
    {
        var record = _db.EmailCodes
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefault();

        if (record == null || record.ExpirationTime < DateTime.Now || record.Code != Code)
        {
            ModelState.AddModelError("", "Невалиден или изтекъл код.");
            return Page();
        }

        var user = await _db.Users.FindAsync(userId);

        await _signInManager.SignInAsync(user, isPersistent: false);

        return LocalRedirect(returnUrl ?? "/");
    }
}