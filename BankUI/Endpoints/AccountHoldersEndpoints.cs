namespace BankUI.Endpoints;

public static class AccountHoldersEndpoints
{
    public const string Add = "/api/accountholders/add";
    public const string Update = "/api/accountholders/update";
    public const string Delete = "/api/accountholders/delete";
    public const string GetAll = "/api/accountholders/get-all";

    public static string DeleteById(int id) => $"/api/accountholders/delete/{id}";
    public static string GetById(int id) => $"/api/accountholders/id/{id}";
}
