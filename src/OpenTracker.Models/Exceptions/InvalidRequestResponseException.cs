using System;

namespace OpenTracker.Models.Exceptions;

public class InvalidRequestResponseException : Exception
{
    public string RequestDescription { get; }
    public string MissingKey { get; }

    public InvalidRequestResponseException(string requestDescription, string missingKey)
        : base($"Request '{requestDescription}' is invalid and does not contain a '{missingKey}' key.")
    {
        RequestDescription = requestDescription;
        MissingKey = missingKey;
    }
}