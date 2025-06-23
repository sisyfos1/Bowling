namespace Bowling.Domain.Exceptions;

public class FrameCompletedException(int frameNumber) : Exception($"Frame {frameNumber} is already complete.");