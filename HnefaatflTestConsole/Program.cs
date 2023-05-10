using System;
using Hnefatafl;

namespace HnefaatflTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var hnefatafl = new Game();

            while (true)
            {
                if(hnefatafl == null)
                    break;
                Console.WriteLine(hnefatafl.Fen);
                Console.WriteLine(GameToAscii(hnefatafl));
                var move = Console.ReadLine();
                if(move=="") break;
                hnefatafl = hnefatafl.Move(move);
            }
        }

        static string GameToAscii(Game game)
        {
            var text = "  +-----------------------+\n";
            for (var y = Game.Size - 1; y >= 0; y--)
            {
                text += y + 1;
                if (y + 1 < 10) text += ' ';
                text += "|";
                for (var x = 0; x < Game.Size; x++)
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