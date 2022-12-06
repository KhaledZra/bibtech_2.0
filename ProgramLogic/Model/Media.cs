using ProgramLogic.Model.Interface;

namespace ProgramLogic.Model;

public class Media : Entity, IMedia
{
    public string Name { get; set; }
}