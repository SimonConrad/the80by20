using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the80by20.Shared.Infrastucture.Modules
{
    public interface IModuleRegistry
    {
        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
        void AddBroadcastAction(Type requestType, Func<object, Task> action);
    }
}
