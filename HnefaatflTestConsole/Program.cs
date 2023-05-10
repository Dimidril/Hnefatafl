using System;
using Hnefatafl;

namespace HnefaatflTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Game hnefatafl = new Game();

            while (true)
            {
                Console.WriteLine(hnefatafl.Fen);
                Console.WriteLine(GameToAscii(hnefatafl));
                string move = Console.ReadLine();
                if(move=="") break;
                hnefatafl = hnefatafl.Move(move);
            }
        }

        static string GameToAscii(Game game)
        {
            string text = "  +-----------------------+\n";
            for (int y = Game.size - 1; y >= 0; y--)
            {
                text += y + 1;
                if (y + 1 < 10) text += ' ';
                text += "|";
                for (int x = 0; x < Game.size; x++)
                {
                    text += game.GetFigureAt(x, y) + " ";
                }

                text += "|\n";
            }

            text += "  +-----------------------+\n";
            text += "   a b c d e f g h i j k  ";
            return text;
        }
    }
}