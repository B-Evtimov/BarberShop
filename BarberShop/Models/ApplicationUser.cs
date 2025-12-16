using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public string Phone { get; set; }
    public bool IsAdmin { get; set; }
}