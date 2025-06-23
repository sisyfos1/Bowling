using Bowling.Domain.Interfaces;
using Bowling.Domain.Models;

namespace Bowling.Domain;

public class FrameStrategy : IFrameStrategy
{
    public int CalculateScore(IReadOnlyList<Roll> frameRolls, IReadOnlyList<Roll> upcomingRolls)
    {
        var baseScore = frameRolls.Sum(r => r.Pins);

        var isStrike = frameRolls.Count == 1 && frameRolls[0].Pins == Frame.MaxPinCount;
        if (isStrike)
        {
            return baseScore + upcomingRolls.Take(2).Sum(r => r.Pins);
        }

        var isSpare = frameRolls.Count == 2 && baseScore == Frame.MaxPinCount;
        if (isSpare && upcomingRolls.Count > 0)
        {
            return baseScore + upcomingRolls[0].Pins;
        }

        return baseScore;
    }

    public bool IsComplete(IReadOnlyList<Roll> rolls)
    {
        if (rolls.Count == 0) 
            return false;
        
        if (rolls[0].Pins == Frame.MaxPinCount) 
            return true;
        
        return rolls.Count == 2;
    }
}