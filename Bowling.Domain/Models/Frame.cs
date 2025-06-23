using Bowling.Domain.Exceptions;
using Bowling.Domain.Interfaces;

namespace Bowling.Domain.Models;

public record Frame
{
    public const int MaxPinCount = 10;
    
    private readonly IFrameStrategy _frameStrategy;
    
    private readonly List<Roll> _rolls = [];
    
    public int Number { get; }
    
    public IReadOnlyList<Roll> Rolls => _rolls;
    
    public bool IsComplete => _frameStrategy.IsComplete(_rolls);
    
    private Frame(int number, IFrameStrategy frameStrategy, List<Roll> rolls)
    {
        if(number is < 1 or > 10)
            throw new ArgumentException("Frame number must be between 1 and 10");

        Number = number;
        _frameStrategy = frameStrategy ?? throw new ArgumentNullException(nameof(frameStrategy));
        _rolls = rolls;
    }
    
    public int CalculateScore(IReadOnlyList<Roll> upcomingRolls)
    {
        return _frameStrategy.CalculateScore(_rolls, upcomingRolls);
    }
    
    public Frame AddRoll(int pinCount)
    {
        if (_frameStrategy.IsComplete(_rolls))
            throw new FrameCompletedException(Number);
        
        var roll = new Roll(pinCount);
        var updatedRolls = _rolls.Append(roll).ToList();
        return new Frame(Number, _frameStrategy, updatedRolls);
    }
    
    public static Frame Create(int number, IFrameStrategy frameStrategy) => new(number, frameStrategy,[]);
}