using Stonk.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Models
{
    public class Result<T> : IResult
    {
        public int Status { get; set; }
        public bool Succedded { get; set; }
        public string Message { get; set; }
        public T Instance { get; set; }

        public object GetResult()
        {
            return Instance;
        }
    }

    public class Result : Result<object>
    {

    }

    public class ResultArray<T> : IResult
    {
        public int Status { get; set; }
        public bool Succedded { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Instances { get; set; }

        public object GetResult()
        {
            return Instances;
        }
    }
}
