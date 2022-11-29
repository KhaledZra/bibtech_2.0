namespace UserInterface.DbManager;

using MySqlConnector;
using Dapper;

public class Database
{
    private MySqlConnectionStringBuilder _mySqlConnectionStringBuilder;
    private MySqlConnection _mySqlConnection;

    public Database()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        _mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "bibtech_two",
            UserID = "root"
        };
        
        try
        {
            _mySqlConnection = new MySqlConnection(_mySqlConnectionStringBuilder.ConnectionString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public List<Account> GetAccountFromDB()
    {
        return _mySqlConnection.Query<Account>("SELECT * FROM account").ToList();
    }
}