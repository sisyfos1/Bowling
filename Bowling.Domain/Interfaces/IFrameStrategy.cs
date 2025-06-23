using Bowling.Domain.Models;

namespace Bowling.Domain.Interfaces;

public interface IFrameStrategy
{
    int CalculateScore(IReadOnlyList<Roll> frameRolls, IReadOnlyList<Roll> upcomingRolls);
    bool IsComplete(IReadOnlyList<Roll> rolls);
}