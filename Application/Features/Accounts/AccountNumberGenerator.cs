namespace Application.Features.Accounts;

public static class AccountNumberGenerator
{
    public static string GenerateAccountNumber()
    {
        var random = new Random();
        var accountNumber = string.Empty;
        for (int i = 0; i < 10; i++)
        {
            accountNumber += random.Next(0, 10).ToString();
        }
        return accountNumber;
    }
}
