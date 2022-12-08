using ProgramLogic.Model.Interface;

namespace ProgramLogic.Model;

public class Library : Entity, ILibrary
{
    public string Name { get; set; }
    public string Adress { get; set; }
    public string City { get; set; }

    // Load contructor for Dapper/Db
    public Library()
    {
        
    }
}