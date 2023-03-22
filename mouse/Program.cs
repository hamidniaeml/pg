using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);

    [StructLayout(LayoutKind.Sequential)]
    struct Point
    {
        public int X;
        public int Y;
    }

    static void Main(string[] args)
    {
        GetCursorPos(out var point);
        var lastX = point.X;

        // Start timer to check for idle mouse
        var timer = new Timer(state =>
        {
            GetCursorPos(out point);
            var currentX = point.X;

            if (currentX == lastX )
                SetCursorPos(currentX + 1, point.Y);

            lastX = currentX;
        }, null, 0, 5*60*1000); // 5 minutes 

        Console.ReadLine();
    }
}