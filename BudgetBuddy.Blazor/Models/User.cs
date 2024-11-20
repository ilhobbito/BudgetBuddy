namespace BudgetBuddy.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Authority Authority { get; set; }
}
public enum Authority
{
    Owner,
    Member,
    Observer
}