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
            return x >= 0 && x < Game.size &&
                   y >= 0 && y < Game.size;
        }

        public bool IsThrone()
        {
            return x == Game.size / 2 && y == Game.size / 2;
        }

        public bool IsСorner()
        {
            return (x == 0 && y == 0) || (x == 0 && y == Game.size - 1) ||
                   (x == Game.size - 1 && y == 0) || (x == Game.size - 1 && y == Game.size - 1);
        }
        
        public static bool operator ==(Square a, Square b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Square a, Square b)
        {
            return !(a == b);
        }
    }
}