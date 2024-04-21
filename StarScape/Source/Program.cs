using System;

using StarScape.Source.World.Tiles;

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
