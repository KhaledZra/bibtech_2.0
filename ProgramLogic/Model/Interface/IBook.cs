namespace ProgramLogic.Model.Interface;

public interface IBook
{
    public string Title { get; set; }
    public bool IsAvailable { get; set; }
    public int MediaId { get; set; }
    public int LibraryId { get; set; }
}