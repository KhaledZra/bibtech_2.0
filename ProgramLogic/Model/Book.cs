using ProgramLogic.Model.Interface;

namespace ProgramLogic.Model;

public class Book : Entity, IBook
{
    public string Title { get; set; }
    public bool IsAvailable { get; set; }
    public int MediaId { get; set; }
    public int LibraryId { get; set; }
    public Media MediaType { get; set; }
    public Library LibraryInfo { get; set; }

    public Book()
    {
        
    }

    public string GetBookString()
    {
        return $"Book ISBN: {this.Id}\n" +
               $"Title: {this.Title}\n" +
               $"Media: {this.MediaType.Name}\n" +
               $"Library Name: {this.LibraryInfo.Name}\n" + 
               $"Available to loan: {this.IsAvailable}\n" +
               "----";
    }
}