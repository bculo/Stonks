using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Contracts
{
    public interface IResult
    {
        public bool Succedded { get; }
        public int Status { get; }
        public string Message { get; }

        object GetResult();
    }
}
