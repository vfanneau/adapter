using System.Diagnostics;

namespace dependency_inversion
{
    public class Program
    {
        public static void Main()
        {
            Terminal terminal = new Terminal();
            while (!terminal.Exited)
            {
                IRunable command = terminal.PromptCommand();
                terminal.ExecuteCommand(command);
            }
        }
    }
}