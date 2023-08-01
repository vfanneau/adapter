using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dependency_inversion
{
    public interface IRunable
    {
        string ToText();
        string Run();
    }
}
