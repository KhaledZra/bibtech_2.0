using UserInterface.Enum;

namespace UserInterface.Handler;

public class UIHandler
{
    private ProgramState _programState;

    public UIHandler()
    {
        _programState = ProgramState.LoginMenu;
    }

    public void RunProgram()
    {
        Console.Clear();
        do { } while (StateHandler());
        Console.WriteLine("Till next time!");
    }
    
    private bool StateHandler()
    {
        if (_programState == ProgramState.LoginMenu)
        {
            Console.WriteLine("Welcome to Bibtech 2.0!");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Quit");
            Console.Write("Choice: ");
            
            int.TryParse(Console.ReadLine(), out int result);
            Console.Clear();

            if (result == 1)
            {
                
            }
            else if (result == 2)
            {
                _programState = ProgramState.Exit;
            }
        }
        else if (_programState == ProgramState.Exit)
        {
            return false;
        }
        else
        {
            throw new NotImplementedException($"State {_programState} is not implemented");
        }
        
        return true;
    }
}