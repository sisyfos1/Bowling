using Bowling.Domain.Models;

namespace Bowling.Domain.Interfaces;

public interface IFrameFactory
{
    Frame Create(int number);
}