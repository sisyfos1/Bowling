using Bowling.Domain.Interfaces;
using Bowling.Domain.Models;

namespace Bowling.Domain;

public class FrameFactory(
    FrameStrategy frameStrategy,
    TenthFrameStrategy tenthStrategy)
    : IFrameFactory
{
    private readonly IFrameStrategy _frameStrategy = frameStrategy;
    private readonly IFrameStrategy _tenthStrategy = tenthStrategy;

    public Frame Create(int number)
    {
        var strategy = number == 10 ? _tenthStrategy : _frameStrategy;
        return Frame.Create(number, strategy);
    }
}