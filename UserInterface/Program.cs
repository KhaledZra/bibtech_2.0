namespace UserInterface;

using UserInterface.Handler;
using DataManager;
internal class Program
{
    static void Main(string[] args)
    {
        //Database db = new Database(); // For debugging
        UIHandler uiHandler = new UIHandler();

        uiHandler.RunProgram();
    }
}