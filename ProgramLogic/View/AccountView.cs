namespace ProgramLogic.View;

using ProgramLogic.Model.Interface;
using ProgramLogic.Handler;

public class AccountView
{
    public static void ShowAccount(IAccount account)
    {
        Console.WriteLine(AccountHandler.GetAccount(account));
    }
}