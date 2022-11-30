using UserInterface.Controller;

namespace UserInterface.View;

public class AccountView
{
    public static void ShowAccount(IAccount account)
    {
        Console.WriteLine(AccountController.GetAccount(account));
    }
}