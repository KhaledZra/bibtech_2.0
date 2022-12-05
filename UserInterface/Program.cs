using System.ComponentModel.Design;

namespace UserInterface;

using UserInterface.Handler;
using DataManager;
internal class Program
{
    static void Main(string[] args)
    {
        Database db = new Database();
        UIHandler uiController = new UIHandler();

        uiController.RunProgram();
    }
}

// var accounts = db.GetAccountFromDb();
//
// foreach (var account in accounts)
// {
//     AccountView.ShowAccount(account);
//     Console.WriteLine("------");
// }

// Account account = new Account
//  (
//      "KalleKid",
//      "test123",
//      "Khaled",
//      "Zraiqi",
//      "Khaled@gmail.com",
//      "0736972075",
//      1
//  );