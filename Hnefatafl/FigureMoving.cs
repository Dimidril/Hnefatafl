namespace Hnefatafl
{
    public class FigureMoving
    {
        public Figure Figure { get; private set; }
        public Square From { get; private set; }
        public Square To { get; private set; }

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