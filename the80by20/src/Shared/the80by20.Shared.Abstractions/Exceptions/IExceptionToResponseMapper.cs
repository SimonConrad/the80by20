using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace the80by20.Shared.Abstractions.Exceptions
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}
