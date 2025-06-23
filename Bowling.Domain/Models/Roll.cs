namespace Bowling.Domain.Models;

public sealed record Roll
{
    public int Pins { get; }

    public Roll(int pins)
    {
        if (pins is < 0 or > Frame.MaxPinCount)
            throw new ArgumentOutOfRangeException(nameof(pins), $"Pin count must be between 0 and {Frame.MaxPinCount}");

        Pins = pins;
    }
}