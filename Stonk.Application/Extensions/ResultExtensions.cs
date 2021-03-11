using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Stonk.Application.Extensions
{
    public static class ResultExtensions
    {
        public static Result Success()
        {
            return new Result
            {
                Succedded = true,
                Status = (int)HttpStatusCode.OK
            };
        }

        public static Result<T> Success<T>(T instance)
        {
            return new Result<T>
            {
                Instance = instance,
                Succedded = true,
                Status = (int)HttpStatusCode.OK
            };
        }

        public static ResultArray<T> Success<T>(IEnumerable<T> instances)
        {
            return new ResultArray<T>
            {
                Instances = instances,
                Succedded = true,
                Status = (int)HttpStatusCode.OK
            };
        }

        public static Result Failure(string message, int status = (int)HttpStatusCode.BadRequest)
        {
            return new Result
            {
                Succedded = false,
                Message = message,
                Status = status
            };
        }

        public static Result<T> Failure<T>(string message, int status = (int)HttpStatusCode.BadRequest)
        {
            return new Result<T>
            {
                Succedded = false,
                Message = message,
                Status = (int)HttpStatusCode.BadRequest
            };
        }

        public static ResultArray<T> FailureArray<T>(string message, int status = (int)HttpStatusCode.BadRequest)
        {
            return new ResultArray<T>
            {
                Succedded = false,
                Message = message,
                Status = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
