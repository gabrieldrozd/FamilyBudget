using FamilyBudget.Common.Results.Core;

namespace FamilyBudget.Common.Api.Envelope;

/// <summary>
/// Response envelope for request that returns no data
/// </summary>
public class Envelope
{
    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Indicates if the request was successful
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Indicates if the request was not successful and contains errors
    /// </summary>
    public bool HasErrors { get; set; }

    /// <summary>
    /// List of errors
    /// </summary>
    public Error[] Errors { get; set; }

    protected Envelope()
    {
        IsSuccess = true;
        Errors = Array.Empty<Error>();
    }

    public Envelope(bool isSuccess, params Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors ?? Array.Empty<Error>();
        HasErrors = errors?.Length > 0;
    }

    public Envelope WithCode(int code)
    {
        StatusCode = code;
        return this;
    }
}