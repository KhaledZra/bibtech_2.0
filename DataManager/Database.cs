using System.Collections.Immutable;

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

    public Book GetBookJoinMediaFromDb(int id)
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
    
    public List<Book> GetBooksJoinMediaAndLibraryFromDb(bool isCheckForAvailability = false, int whereLibId = 0)
    {
        string sqlCode;
        
        if (isCheckForAvailability && whereLibId != 0)
        {
            sqlCode = "SELECT b.*, m.*, l.* " +
                      "FROM book b " +
                      "INNER JOIN media m on b.media_id = m.id " +
                      "INNER JOIN library l on b.library_id = l.id " +
                      $"WHERE B.is_available = 1 AND b.library_id = {whereLibId};";
        }
        else if (isCheckForAvailability && whereLibId == 0)
        {
            sqlCode = "SELECT b.*, m.*, l.* " +
                      "FROM book b " +
                      "INNER JOIN media m on b.media_id = m.id " +
                      "INNER JOIN library l on b.library_id = l.id " +
                      $"WHERE B.is_available = 1;";
        }
        else
        {
            sqlCode = "SELECT b.*, m.*, l.* " +
                      "FROM book b " +
                      "INNER JOIN media m on b.media_id = m.id " +
                      "INNER JOIN library l on b.library_id = l.id;";
        }

        var result =_mySqlConnection.Query<Book, Media, Library, Book>(sqlCode, (b, m, l) =>
            {
                b.MediaType = m;
                b.LibraryInfo = l;
                return b;
            },
            splitOn: "library_id, name"
        ).AsQueryable();

        return result.ToList();
    }
    
    public List<Book> GetBooksWhereLibFromDb(int whereLibId, bool isCheckForAvailability = false)
    {
        string sqlCode;
        
        if (isCheckForAvailability && whereLibId != 0)
        {
            sqlCode = "SELECT b.*, m.*, l.* " +
                      "FROM book b " +
                      "INNER JOIN media m on b.media_id = m.id " +
                      "INNER JOIN library l on b.library_id = l.id " +
                      $"WHERE b.is_available = 1 AND b.library_id = {whereLibId};";
        }
        else
        {
            sqlCode = "SELECT b.*, m.*, l.* " +
                      "FROM book b " +
                      "INNER JOIN media m on b.media_id = m.id " +
                      "INNER JOIN library l on b.library_id = l.id " +
                      $"WHERE b.library_id = {whereLibId};";
        }

        var result =_mySqlConnection.Query<Book, Media, Library, Book>(sqlCode, (b, m, l) =>
            {
                b.MediaType = m;
                b.LibraryInfo = l;
                return b;
            },
            splitOn: "library_id, name"
        ).AsQueryable();

        return result.ToList();
    }
    
    public List<Book> GetBooksWhereMediaFromDb(int whereMediaId, bool isCheckForAvailability = false)
    {
        string sqlCode;
        
        if (isCheckForAvailability && whereMediaId != 0)
        {
            sqlCode = "SELECT b.*, m.*, l.* " +
                      "FROM book b " +
                      "INNER JOIN media m on b.media_id = m.id " +
                      "INNER JOIN library l on b.library_id = l.id " +
                      $"WHERE b.is_available = 1 AND media_id = {whereMediaId};";
        }
        else
        {
            sqlCode = "SELECT b.*, m.*, l.* " +
                      "FROM book b " +
                      "INNER JOIN media m on b.media_id = m.id " +
                      "INNER JOIN library l on b.library_id = l.id " +
                      $"WHERE b.media_id = {whereMediaId};";
        }

        var result =_mySqlConnection.Query<Book, Media, Library, Book>(sqlCode, (b, m, l) =>
            {
                b.MediaType = m;
                b.LibraryInfo = l;
                return b;
            },
            splitOn: "library_id, name"
        ).AsQueryable();

        return result.ToList();
    }
    
    public List<Book> GetBooksJoinMediasFromDb()
    {
        string sqlCode = "SELECT b.*, m.* FROM book b INNER JOIN media m on b.media_id = m.id;";

        // Uses Delegate Func to map tables and takes an anonymous method
        var result =_mySqlConnection.Query<Book, Media, Book>(sqlCode, (b, m) =>
            { 
                b.MediaType = m;
                return b;
            }, // lambda output
            splitOn: "media_id"
        ).AsQueryable(); // in this case modifies book and throws it out with the new selected data from DB

        return result.ToList();
    }

    public List<Library> GetLibrariesFromDb()
    {
        return _mySqlConnection.Query<Library>("SELECT * FROM library").ToList();
    }
    
    public List<Media> GetMediasFromDb()
    {
        return _mySqlConnection.Query<Media>("SELECT * FROM media").ToList();
    }

    public List<Loan> GetLoansFromDb()
    {
        return _mySqlConnection.Query<Loan>("SELECT * FROM loan").ToList();
    }
    
    public List<Loan> GetJoinedCustomerBookLoansFromDb(int customerId)
    {
        string sqlCode = "SELECT l.*, b.*, c.* FROM loan l " +
                         "INNER JOIN book b ON l.book_id = b.id " +
                         "INNER JOIN customer c ON l.customer_id = c.id "+
                         $"WHERE l.customer_id = {customerId};";
        
        
        var result = _mySqlConnection.Query<Loan, Book, Account, Loan>(sqlCode,
            (l, b, c) =>
            {
                l.LoanedBook = b;
                l.Customer = c;
                return l;
            },
            splitOn: "customer_id, library_id"
            ).AsQueryable();

        return result.ToList();
    }

    public void InsertLoanToDb(Loan loan)
    {
        string sqlCode = @"INSERT INTO loan (start_date, due_date, is_returned, book_id, customer_id)
                            values (@StartDate, @DueDate, @IsReturned, @BookId, @CustomerId);";
        _mySqlConnection.Execute(sqlCode, loan);
    }

    public void UpdateBookAvailability(bool isAvailable, int bookId)
    {
        _mySqlConnection.Execute($"UPDATE book SET book.is_available = {isAvailable} WHERE book.id = {bookId}");
    }
}