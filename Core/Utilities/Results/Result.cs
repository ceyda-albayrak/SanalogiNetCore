using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Utilities.Results
{
    public class Result<T>:IResult<T>
    {
        [JsonConstructor]
        public Result(bool responseStatus, string errorGuid, string message,T results )
        {
            ResponseStatus = responseStatus;
            ErrorGUID = errorGuid;
            Message = message;
            Results = results;
        }

        public bool ResponseStatus { get; }
        public string ErrorGUID { get; }
        public string Message { get; }
        public T Results { get; }

    }
}
