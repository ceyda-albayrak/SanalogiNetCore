using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{ 
    public class Result : IResult
    {
        
         
        public Result(bool success, string message):this(success)
        {
            Message = message;
            Succes = success;
        }
        public Result(bool success)
        {
            Succes = success;
        }
        public bool Succes { get; }

        public string Message { get;  }
    }
}
