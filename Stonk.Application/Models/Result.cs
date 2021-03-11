using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Models
{
    public class Result<T>
    {
        public bool Succedded { get; set; }
        public string Message { get; set; }
        public T Instance { get; set; }
    }

    public class Result : Result<object>
    {

    }
}
