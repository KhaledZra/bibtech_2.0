namespace ProgramLogic.Handler;

using ProgramLogic.Model.Interface;

public class AccountHandler
{
    public static string GetAccount(IAccount account)
    {
        string returnString = $"Pin code: {account.PinCode}\n" +
                              $"Name: {account.FirstName}{account.LastName}\n" +
                              $"Email: {account.Email}\n" +
                              $"Phone number: {account.PhoneNumber}\n";


        return returnString;
    }
}