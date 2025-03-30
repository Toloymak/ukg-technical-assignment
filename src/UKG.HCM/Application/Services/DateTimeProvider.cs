namespace UKG.HCM.Application.Services;

/// Provides the current date and time
public interface IProvideCurrentDateTime
{
    /// Provides the current date and time as a <see cref="DateTimeOffset"/>.
    DateTimeOffset DateTimeOffsetNow();
}

/// <inheritdoc />
internal class DateTimeProvideCurrent : IProvideCurrentDateTime
{
    /// <inheritdoc />
    public DateTimeOffset DateTimeOffsetNow()
        => DateTimeOffset.Now;
}