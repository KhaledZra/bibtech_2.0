namespace UserInterface.Controller;

public class AccountController
{
    public static string GetAccount(IAccount account)
    {
        string returnString = $"Username: {account.UserName}\n" +
                              $"Password: {account.Password}\n" +
                              $"Name: {account.FirstName} {account.LastName}\n" +
                              $"Email: {account.Email}\n" +
                              $"Phone number: {account.PhoneNumber}\n" +
                              $"Permission ID: {account.PermissionId}";
            
            
        return returnString;
    }
}