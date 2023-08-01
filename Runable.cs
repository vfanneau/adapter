using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dependency_inversion
{
    internal class Runable : IRunable
    {
        private readonly Command _command;

        public Runable (Command command)
        {
            _command = command;
        }

        public string ToText()
        {
            return this._command.ToString();
        }

        public string Run()
        {
            this._command.Launch();
            return this._command.Output;
        }
    }
}
