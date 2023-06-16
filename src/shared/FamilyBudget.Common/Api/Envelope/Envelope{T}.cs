using FamilyBudget.Common.Results.Core;

namespace FamilyBudget.Common.Api.Envelope;

/// <summary>
/// Response envelope for request that returns data
/// </summary>
/// <typeparam name="T">Type of data</typeparam>
public class Envelope<T> : Envelope
{
    /// <summary>
    /// Data of type T
    /// </summary>
    public T Data { get; set; }

    public Envelope(T data)
    {
        Data = data;
    }

    public Envelope(T data, bool isSuccess, Error error = null) : base(isSuccess, error)
    {
        Data = data;
    }

    public Envelope(T data, bool isSuccess, Error[] errors = null) : base(isSuccess, errors)
    {
        Data = data;
    }

    public Envelope(Error error = null) : base(false, error)
    {
        Data = default;
    }

    public Envelope(Error[] errors = null) : base(false, errors)
    {
        Data = default;
    }

    public new Envelope<T> WithCode(int code)
    {
        StatusCode = code;
        return this;
    }
}