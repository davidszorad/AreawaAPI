namespace Areawa.CLI;

public class Spinner : IDisposable
{
    private const string Sequence = @"/-\|";
    private int counter = 0;
    private readonly int left;
    private readonly int top;
    private readonly int delay;
    private bool active;
    private readonly Thread thread;

    public Spinner()
    {
        delay = 100;
        left = Console.GetCursorPosition().Left;
        top = Console.GetCursorPosition().Top;
        thread = new Thread(Spin);
    }

    public void Start()
    {
        active = true;
        if (!thread.IsAlive)
            thread.Start();
    }

    public void Stop()
    {
        active = false;
        Draw(' ');
    }

    private void Spin()
    {
        while (active)
        {
            Turn();
            Thread.Sleep(delay);
        }
    }

    private void Draw(char c)
    {
        Console.SetCursorPosition(left, top);
        Console.Write(c);
    }

    private void Turn()
    {
        Draw(Sequence[++counter % Sequence.Length]);
    }

    public void Dispose()
    {
        Stop();
    }
}