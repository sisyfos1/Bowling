using Bowling.Domain.Interfaces;
using Bowling.Domain.Models;

namespace Bowling.Domain;

public class TenthFrameStrategy: IFrameStrategy
{
    public int CalculateScore(IReadOnlyList<Roll> frameRolls, IReadOnlyList<Roll> _)
    {
        return frameRolls.Sum(r => r.Pins);
    }

    public bool IsComplete(IReadOnlyList<Roll> frameRolls)
    {
        if (frameRolls.Count < 2)
            return false;

        var first = frameRolls[0].Pins;
        var second = frameRolls[1].Pins;
        
        var isStrike = first == Frame.MaxPinCount;
        var isSpare = first + second == Frame.MaxPinCount;
        
        if (isStrike || isSpare)
            return frameRolls.Count == 3;
        
        return frameRolls.Count == 2;
    }
}