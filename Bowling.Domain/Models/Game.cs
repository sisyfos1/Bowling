using Bowling.Domain.Interfaces;

namespace Bowling.Domain.Models;

public sealed record Game
{
    private readonly List<Frame> _frames = [];
    public IReadOnlyList<Frame> Frames => _frames;
    private readonly IFrameFactory _frameFactory;
    
    private Game(IFrameFactory frameFactory)
    {
        _frameFactory = frameFactory;
    }

    public void Roll(int frameNumber)
    {
        var frame = _frameFactory.Create(frameNumber);
        var pins = Player.Throw();
        while (!frame.IsComplete)
        {
            frame = frame.AddRoll(pins);
            pins = Player.Throw(Frame.MaxPinCount - pins);
        }
        _frames.Add(frame);
    }

    public int CalculateScore()
    {
        var allRolls = _frames.SelectMany(f => f.Rolls).ToList();
        var totalScore = 0;
        var rollIndex = 0;

        foreach (var frame in _frames)
        {
            var frameRolls = frame.Rolls;
            var upcomingRolls = allRolls.Skip(rollIndex + frameRolls.Count).ToList();

            totalScore += frame.CalculateScore(upcomingRolls);
            rollIndex += frameRolls.Count;
        }

        return totalScore;
    }
    
    public static Game Create(IFrameFactory frameFactory) => new(frameFactory);
}