using Bowling.Application;
using Bowling.Domain.Models;

Console.WriteLine("Ten pin bowling game.");

var gameService = new GameService();

for (var frame = 1; frame <= 10; frame++)
{
    var scoreboard = gameService.Play(frame);

    PrintScoreboard(scoreboard.frames);
    Console.WriteLine($"Total Score: {scoreboard.score}");
    Console.WriteLine(new string('-', 40));
}

return;

static void PrintScoreboard(IReadOnlyList<Frame> frames)
{
    foreach (var frame in frames)
    {
        Console.Write($"Frame {frame.Number}: ");
        Console.WriteLine(FormatFrameRolls(frame));
    }
}

static string FormatFrameRolls(Frame frame)
{
    return frame.Number < 10 ? FormatStandardFrame(frame) : FormatTenthFrame(frame);
}

static string FormatStandardFrame(Frame frame)
{
    var rolls = frame.Rolls;

    if (rolls.Count == 1 && rolls[0].Pins == Frame.MaxPinCount)
        return "X";

    if (rolls.Count != 2) 
        return string.Join(" ", rolls.Select(r => r.Pins));
    
    var first = rolls[0].Pins;
    var second = rolls[1].Pins;

    return first + second == Frame.MaxPinCount ? $"{first} /" : $"{first} {second}";
}

static string FormatTenthFrame(Frame frame)
{
    var rolls = frame.Rolls;
    var result = new List<string>();

    for (int i = 0; i < rolls.Count; i++)
    {
        var roll = rolls[i];

        if (roll.Pins == Frame.MaxPinCount)
        {
            result.Add("X");
        }
        else if (i > 0 && rolls[i - 1].Pins + roll.Pins == Frame.MaxPinCount && rolls[i - 1].Pins != Frame.MaxPinCount)
        {
            result.Add("/"); // Spare
        }
        else
        {
            result.Add(roll.Pins.ToString());
        }
    }

    return string.Join(" ", result);
}