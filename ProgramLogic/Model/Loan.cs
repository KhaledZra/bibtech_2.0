namespace ProgramLogic.Model;

public class Loan : Entity
{
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsReturned { get; set; }
    public int BookId { get; set; }
    public int CustomerId { get; set; }

    public Book LoanedBook { get; set; }
    public Account Customer { get; set; }

    public Loan(int bookId, int customerId)
    {
        StartDate = DateTime.Today;
        DueDate = StartDate.AddMonths(1);
        IsReturned = false;
        BookId = bookId;
        CustomerId = customerId;
    }

    // To load Loans from DB with Dapper
    public Loan()
    {
        
    }

    public string GetLoanString()
    {
        return $"Loan ID: {Id}\n" +
               $"Book ISBN: {BookId}\n" +
               $"Book Title: {LoanedBook.Title}\n" +
               $"Due Date: {DueDate.Month}/{DueDate.Day}/{DueDate.Year}\n" +
               "-----";
    }
}