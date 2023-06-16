using FamilyBudget.Common.Results.Core;

namespace FamilyBudget.Common.Api.Envelope;

/// <summary>
/// Response envelope for request that returns no data
/// </summary>
public class EmptyEnvelope : Envelope<EmptyData>
{
    public EmptyEnvelope()
        : base(new EmptyData())
    {
    }

    public EmptyEnvelope(Error error)
        : base(error)
    {
    }

    public EmptyEnvelope(Error[] errors)
        : base(errors)
    {
    }
}