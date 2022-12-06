using ProgramLogic.Model.Interface;

namespace ProgramLogic.Model;

public class Book : Entity, IBook
{
    public string Title { get; set; }
    public bool IsAvailable { get; set; }
    public int MediaId { get; set; }
    public int LibraryId { get; set; }
    
    public Media MediaType { get; set; }
    // library obj?

    public Book()
    {
        
    }
}