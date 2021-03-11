using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stonk.Application.Extensions
{
    public static class ResultExtensions
    {
        public static Result Success()
        {
            return new Result
            {
                Succedded = true
            };
        }

        public static Result<T> Success<T>(T instance)
        {
            return new Result<T>
            {
                Instance = instance,
                Succedded = true
            };
        } 

        public static Result Failure(string message)
        {
            return new Result
            {
                Succedded = false,
                Message = message
            };
        }

        public static Result<T> Failure<T>(string message)
        {
            return new Result<T>
            {
                Succedded = true,
                Message = message
            };
        }
    }
}
