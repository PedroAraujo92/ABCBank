namespace Domain;

public class AccountHolder : Person
{
    public string ContactNumber { get; set; }
    public string EmailAddress { get; set; }
    public List<Account> Accounts { get; set; } = [];

    public AccountHolder Update(string firstName, string lastName, string contactNumber, string emailAddress)
    {
        FirstName = string.IsNullOrWhiteSpace(firstName) ? FirstName : firstName;
        LastName = string.IsNullOrWhiteSpace(lastName) ? LastName : lastName;
        ContactNumber = string.IsNullOrWhiteSpace(contactNumber) ? ContactNumber : contactNumber;
        EmailAddress = string.IsNullOrWhiteSpace(emailAddress) ? EmailAddress : emailAddress;
        return this;
    }
}
