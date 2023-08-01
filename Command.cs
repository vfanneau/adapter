using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dependency_inversion
{
    public class Command
    {
        public string Executable { get; private set; }
        public string[] Arguments { get; private set; }
        public string Output { get; private set; }

        public Command(string line)
        {
            string[] splittedLine = line.Split(' ');
            Executable = splittedLine[0];
            Arguments = splittedLine.Skip(1).ToArray();
            Output = "";
        }

        public override string ToString()
        {
            if (ExecutableFullPath is null)
            {
                return Executable;
            }
            else
            {
                return ExecutableFullPath;
            }
        }

        public void Launch()
        {
            string commandOutput;
            if (Executable.Length == 0)
            {
                return;
            }
            using (Process process = new Process())
            {
                process.StartInfo.FileName = ExecutableFullPath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.Arguments = String.Join(' ', Arguments);
                process.Start();

                commandOutput = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            Output = commandOutput;
        }

        public string ExecutableFullPath
        {
            get
            {
                string executableWithExtension;
                if (Executable.EndsWith(".exe"))
                {
                    executableWithExtension = Executable;
                }
                else
                {
                    executableWithExtension = Executable + ".exe";
                }
                if (File.Exists(executableWithExtension))
                {
                    return Path.GetFullPath(Executable);
                }

                string values = Environment.GetEnvironmentVariable("PATH");
                foreach (var path in values.Split(Path.PathSeparator))
                {
                    string fullPath = Path.Combine(path, executableWithExtension);
                    if (File.Exists(fullPath))
                    {
                        return fullPath;
                    }
                }
                return null;
            }
        }
    }
}
