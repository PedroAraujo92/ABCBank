namespace Domain;

public class AccountHolder: Person
{
    public string ContactNumber { get; set; }
    public string EmailAddress { get; set; }
    public List<Account> Accounts { get; set; } = [];
}
