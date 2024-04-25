using System;


namespace StarScape
{
    public static class Program
    {
        [MTAThread]
        static void Main()
        {
            using (var game = new MainGame())
                game.Run();
        }
    }
}
