using Bowling.Domain;
using Bowling.Domain.Models;

namespace Bowling.Application;

public class GameService
{
    private readonly Game _game = Game.Create(new FrameFactory(new FrameStrategy(), new TenthFrameStrategy()));
    
    public (IReadOnlyList<Frame> frames, int score) Play(int frame)
    {
        _game.Roll(frame);
        return new (_game.Frames, _game.CalculateScore());
    }
}