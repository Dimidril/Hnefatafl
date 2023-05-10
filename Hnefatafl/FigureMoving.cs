using System;

namespace Hnefatafl
{
    public class FigureMoving
    {
        public Figure Figure { get; private set; }
        public Square From { get; private set; }
        public Square To { get; private set; }

        public int DeltaX => To.x - From.x;
        public int DeltaY => To.y - From.y;
        
        public int AbsDeltaX => Math.Abs(DeltaX);
        public int AbsDeltaY => Math.Abs(DeltaY);
        
        public int SignX => Math.Sign(DeltaX);
        public int SignY => Math.Sign(DeltaY);

        public FigureMoving(FigureOnSquare figureOnSquare, Square to)
        {
            Figure = figureOnSquare.Figure;
            From = figureOnSquare.Square;
            To = to;
        }

        public FigureMoving(string move) //D e2 e4
        {
            string[] text = move.Split(" ");
            Figure = (Figure)text[0][0];
            From = new Square(text[1]);
            To = new Square(text[2]);
            
        }
    }
}