using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the80by20.Shared.Abstractions
{
    public interface IDataSeeder
    {
        Task Seed(IServiceScope scope, CancellationToken cancellationToken);
    }
}
