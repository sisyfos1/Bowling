using Bowling.Domain.Models;

namespace Bowling.Domain;

public abstract class Player
{
    private static readonly Random Random = new();

    public static int Throw(int remainingPins = Frame.MaxPinCount)
    {
        if (remainingPins is < 0 or > Frame.MaxPinCount)
            throw new ArgumentOutOfRangeException(nameof(remainingPins), $"Must be between 0 and {Frame.MaxPinCount}");

        return Random.Next(0, remainingPins + 1);
    }
}