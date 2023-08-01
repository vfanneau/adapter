using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dependency_inversion
{
    public class Terminal
    {
        public bool Exited { get; set; }
        private string _promptString;

        public Terminal()
        {
            _promptString = String.Format("{0}$ ", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            Exited = false;
        }

        public IRunable PromptCommand()
        {
            string commandLine = Prompt();
            return new Runable(new Command(commandLine));
        }

        public string Prompt()
        {
            Console.Write(_promptString);
            string userCommand = Console.ReadLine();
            return userCommand;
        }

        public void ExecuteCommand(IRunable command)
        {
            try
            {
                string output = command.Run();
                if (output.Length > 0)
                {
                    Console.WriteLine(output);
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.Error.WriteLine("{0}: path not found", command);
            }
        }
    }
}
