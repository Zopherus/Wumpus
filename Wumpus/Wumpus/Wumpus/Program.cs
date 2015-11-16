using System;

namespace Wumpus
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static WumpusGame game;
        static void Main(string[] args)
        {
            using (game = new WumpusGame())
            {
                game.Run();
            }
        }
    }
#endif
}

