using System;
using System.Text.RegularExpressions;

namespace Hnefatafl
{
    public struct Square
    {
        public static Square none = new Square(-1, -1);
        public int x { get; private set; }
        public int y { get; private set; }

        public Square(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Square(string e2)
        {
            char letter = e2[0];
            int index = 1;
            if (Char.IsDigit(letter)) {
                letter = e2[e2.Length - 1];
                index = 0;
            }
            int number = int.Parse(e2.Substring(index, e2.Length - 1));
            x = letter - 'a';
            y = number - 1;
        }
        
        public bool OnBoard()
        {
            return x >= 0 && x < 11 &&
                   y >= 0 && y < 11;
        }
    }
}