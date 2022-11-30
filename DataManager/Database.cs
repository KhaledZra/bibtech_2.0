namespace DataManager;

using MySqlConnector;
using Dapper;
using ProgramLogic.Model;


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
        
        _mySqlConnection = new MySqlConnection(_mySqlConnectionStringBuilder.ConnectionString);
        
        try
        {
            _mySqlConnection.Open(); // Checks if connection can be opened
        }
        catch
        {
            throw new Exception("Connection to DB failed!");
        }
    }

    public List<Account> GetAccountFromDb()
    {
        return _mySqlConnection.Query<Account>("SELECT * FROM account").ToList();
    }
}