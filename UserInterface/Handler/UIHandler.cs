using System.Threading.Channels;
using DataManager;
using ProgramLogic.Model;
using UserInterface.ConsoleHandler;
using UserInterface.Enum;

namespace UserInterface.Handler;

public class UIHandler
{
    private ProgramState _programState;
    private Database _db;
    private Account _loggedInAccount;

    public UIHandler()
    {
        _programState = ProgramState.StartMenu;
        _db = new Database();
    }

    public void RunProgram()
    {
        Console.Clear();
        do { } while (StateHandler());
        Console.WriteLine("Till next time!");
    }
    
    private bool StateHandler()
    {
        if (_programState == ProgramState.StartMenu)
        {
            Output.StartMenu();
            int.TryParse(Console.ReadLine(), out int result);
            Console.Clear();

            if (result == 1) _programState = ProgramState.LoginMenu;
            else if (result == 2) _programState = ProgramState.Exit;
            else Console.WriteLine("Wrong choice!\n---------");
        }
        else if (_programState == ProgramState.LoginMenu)
        {
            Console.Write("Enter id: ");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Enter 4 digit pin code: ");
            int.TryParse(Console.ReadLine(), out int pinCode);
            Console.Clear();

            if (id != 0 || pinCode != 0)
            {
                if (_db.VerifyCustomerLogin(id, pinCode))
                {
                    _loggedInAccount = _db.GetCustomerFromDb(id);
                    _programState = ProgramState.MainMenu;
                }
                else
                {
                    Console.WriteLine("Failed to login! Incorrect Id or Pin code!");
                    Console.WriteLine("--------");
                    _programState = ProgramState.StartMenu;
                }
            }
            else
            {
                Console.WriteLine("Failed to login! Incorrect Id or Pin code!");
                _programState = ProgramState.StartMenu;
            }
        }
        else if (_programState == ProgramState.MainMenu)
        {
            Console.WriteLine("--------");
            Console.WriteLine($"Hello {_loggedInAccount.FirstName}. Welcome to bibtech 2.0!");
            Console.WriteLine("1. Show all books");
            Console.WriteLine("2. Show all available books");
            Console.WriteLine("3. Show all books from a specific library");
            Console.WriteLine("4. Show all books with specific media type");
            Console.WriteLine("5. Order & Get a book from your chosen store!");
            Console.WriteLine("6. Show all your active loans");
            Console.WriteLine("9. Log out");
            Console.Write("Choice: ");
            int.TryParse(Console.ReadLine(), out int result);
            Console.Clear();

            if (result == 1)
            {
                _db.GetBooksJoinMediaAndLibraryFromDb().ForEach(book => 
                    Console.WriteLine(book.GetBookString()));
            }
            else if (result == 2)
            {
                _db.GetBooksJoinMediaAndLibraryFromDb(true).ForEach(book =>
                    Console.WriteLine(book.GetBookString()));
            }
            else if (result == 3)
            {
                var libs = _db.GetLibrariesFromDb();
                Console.Clear();
                Console.WriteLine("Pick a library to see what books they have to offer:");
                libs.ForEach(library => 
                    Console.WriteLine($"{library.Id}. {library.Name}"));
                if (int.TryParse(Console.ReadLine(), out int libChoice))
                {
                    if (libChoice <= libs.Count && libChoice > 0)
                    {
                        Console.Clear();
                        _db.GetBooksWhereLibFromDb(libChoice).ForEach(book => 
                            Console.WriteLine(book.GetBookString()));
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Error!");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error!");
                }
            }
            else if (result == 4)
            {
                var medias = _db.GetMediasFromDb();
                Console.Clear();
                Console.WriteLine("Pick which media type you are looking for:");
                medias.ForEach(library => 
                    Console.WriteLine($"{library.Id}. {library.Name}"));
                if (int.TryParse(Console.ReadLine(), out int mediaChoice))
                {
                    if (mediaChoice <= medias.Count && mediaChoice > 0)
                    {
                        Console.Clear();
                        _db.GetBooksWhereMediaFromDb(mediaChoice).ForEach(book => 
                            Console.WriteLine(book.GetBookString()));
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Error!");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error!");
                }
            }
            else if (result == 5)
            {
                var libs = _db.GetLibrariesFromDb();
                Console.Clear();
                Console.WriteLine("Pick which Library you wanna order from");
                libs.ForEach(library => 
                    Console.WriteLine($"{library.Id}. {library.Name}"));
                
                if (int.TryParse(Console.ReadLine(), out int libChoice))
                {
                    if (libChoice <= libs.Count && libChoice > 0)
                    {
                        Console.Clear();
                        var books = _db.GetBooksWhereLibFromDb(libChoice, true);
                        
                        books.ForEach(book => 
                            Console.WriteLine(book.GetBookString()));
                        Console.WriteLine("Enter 0 to cancel!");
                        Console.Write("Pick with ISBN:");
                        if (int.TryParse(Console.ReadLine(), out int isbnChoice))
                        {
                            if (isbnChoice == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Cancelled!");
                                return true;
                            }
                            
                            var pickedBook = books.Where(b => b.IsAvailable && b.Id == isbnChoice).ToList().FirstOrDefault();
                            if (pickedBook != null)
                            {
                                _db.UpdateBookAvailability(false, pickedBook.Id);
                                _db.InsertLoanToDb(new Loan(pickedBook.Id, _loggedInAccount.Id));
                                Console.Clear();
                                Console.WriteLine("Loan created! Please remember to pickup your book within 24 hours!");
                                Console.WriteLine("If 24 hours passes and you fail to pickup the loan will be cancelled!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Error!");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Error!");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Error!");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error!");
                }
            }
            else if (result == 6)
            {
                foreach (var loan in _db.GetJoinedCustomerBookLoansFromDb(_loggedInAccount.Id))
                {
                    if (loan.IsReturned == false)
                    {
                        Console.WriteLine(loan.GetLoanString());
                    }
                }
            }
            else if (result == 9)
            {
                _programState = ProgramState.StartMenu;
                _loggedInAccount = null;
                Console.WriteLine("Logged out!");
                Console.WriteLine("--------");
            }
        }
        else if (_programState == ProgramState.Exit) return false;
        else
        {
            throw new NotImplementedException($"State {_programState} is not implemented");
        }
        
        return true;
    }
}