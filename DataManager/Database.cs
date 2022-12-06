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

    public Account GetCustomerFromDb(int id)
    {
        List <Account> result = _mySqlConnection.Query<Account>($"SELECT * FROM customer WHERE id = {id};").ToList();
        if (result.Count() == 1)
        {
            return result[0];
        }
        else
        {
            throw new Exception("Error fetching customer from DB");
        }
    }

    public bool VerifyCustomerLogin(int id, int pinCode)
    {
        string sqlCode = $"SELECT COUNT(id) FROM customer WHERE id = {id} AND pin_code = {pinCode};";
        int result = _mySqlConnection.Query<int>(sqlCode).FirstOrDefault();

        if (result == 1) return true;
        return false;
    }

    public Book GetBookFromDb(int id)
    {
        string sqlCode = $"SELECT b.*, m.* FROM book b INNER JOIN media m on b.media_id = m.id WHERE b.id = {id};";

        var result =_mySqlConnection.Query<Book, Media, Book>(sqlCode, (b, m) =>
            {
                b.MediaType = m;
                return b;
            },
            splitOn: "media_id"
            ).AsQueryable();

        return result.ToList().FirstOrDefault();
    }
    
    public List<Book> GetBooksFromDb()
    {
        string sqlCode = $"SELECT b.*, m.* FROM book b INNER JOIN media m on b.media_id = m.id;";

        var result =_mySqlConnection.Query<Book, Media, Book>(sqlCode, (b, m) =>
            {
                b.MediaType = m;
                return b;
            },
            splitOn: "media_id"
        ).AsQueryable();

        return result.ToList();
    }
}