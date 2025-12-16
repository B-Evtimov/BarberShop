using BarberShop.Models;

public class Reservation
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int BarberId { get; set; }
    public Barber Barber { get; set; }

    public DateTime Date { get; set; }

    public ServiceType Service { get; set; }
    public decimal Price { get; set; }
}