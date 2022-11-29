using Dapper;
using UserInterface.DbManager;
using UserInterface.View;

namespace UserInterface;

internal class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        Database db = new Database();

        Console.WriteLine(db.GetAccountFromDB().Count);

        Account account = new Account
         (
             "KalleKid",
             "test123",
             "Khaled",
             "Zraiqi",
             "Khaled@gmail.com",
             "0736972075",
             1
         );

        AccountView.ShowAccount(account);
    }
}