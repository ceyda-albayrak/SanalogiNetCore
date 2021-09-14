using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IResult<T>
    {
        bool ResponseStatus { get; }
        string ErrorGUID { get; }
        string Message { get; }
        T Results { get; }
    }
}
