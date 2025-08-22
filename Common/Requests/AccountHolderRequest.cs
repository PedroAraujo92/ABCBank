namespace Common.Requests;

public record CreateAccountHolder(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string EmailAddress,
    string ContactNumber);

public record UpdateAccountHolder(
    int Id,
    string FirstName,
    string LastName,
    string EmailAddress,
    string ContactNumber);