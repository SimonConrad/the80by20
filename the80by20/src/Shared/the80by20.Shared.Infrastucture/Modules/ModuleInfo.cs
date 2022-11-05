using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the80by20.Shared.Infrastucture.Modules
{
    internal record ModuleInfo(string Name, string Path, IEnumerable<string> Policies);
}
