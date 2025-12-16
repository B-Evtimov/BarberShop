public class EmailCode
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string Code { get; set; }
    public DateTime ExpirationTime { get; set; }
}