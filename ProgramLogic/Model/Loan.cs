namespace ProgramLogic.Model;

public class Loan : Entity
{
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsReturned { get; set; }
    public int BookId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReturnedDate { get; set; }

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

    public void SetReturnDate()
    {
        ReturnedDate = DateTime.Today;
    }

    public string GetLoanString()
    {
        if (IsReturned == false)
        {
            return $"Loan ID: {Id}\n" +
                   $"Book ISBN: {BookId}\n" +
                   $"Book Title: {LoanedBook.Title}\n" +
                   $"Start Date: {StartDate.Month}/{StartDate.Day}/{StartDate.Year}\n" +
                   $"Due Date: {DueDate.Month}/{DueDate.Day}/{DueDate.Year}\n" +
                   $"Loan returned: {IsReturned}\n" +
                   "-----";
        }
        
        // When loan IsReturned == true
        return $"Loan ID: {Id}\n" +
               $"Book ISBN: {BookId}\n" +
               $"Book Title: {LoanedBook.Title}\n" +
               $"Start Date: {StartDate.Month}/{StartDate.Day}/{StartDate.Year}\n" +
               $"Due Date: {DueDate.Month}/{DueDate.Day}/{DueDate.Year}\n" +
               $"Returned Date: {ReturnedDate.Month}/{ReturnedDate.Day}/{ReturnedDate.Year}\n" +
               $"Loan returned: {IsReturned}\n" +
               "-----";
    }
}