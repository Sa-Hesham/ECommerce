using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Errors;

public class Result
{
    protected Result(bool isSuccess, Error? error = null)
    {
        if (isSuccess && error is not null)
            throw new ArgumentException(
                "A successful result cannot contain an error.",
                nameof(error));

        if (!isSuccess && error is null)
            throw new ArgumentException(
                "A failed result must contain an error.",
                nameof(error));

        IsSuccsess = isSuccess;
        Error = error;
    }

    protected Result() { }

    public bool IsSuccsess { get; }

    public Error ? Error { get; }   
    public bool IsFail =>!IsSuccsess;
   
   
}
