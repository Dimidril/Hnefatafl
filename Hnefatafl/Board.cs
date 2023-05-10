using System;
using System.Text;

namespace Hnefatafl
{
    public class Board
    {
        private Figure[,] _figures;
        
        public string Fen { get; private set; }
        public Color MoveColor { get; private set; }
        public int MoveNumber { get; private set; }

        public Board(string fen)
        {
            Fen = fen;
            _figures = new Figure[Game.size, Game.size];
            Init();
        }

        private void Init()
        {
            string[] parts = Fen.Split();
            InitFigures(parts[0]);
            MoveColor = parts[1] == "w" ? Color.White: Color.Black;
            MoveNumber = int.Parse(parts[2]);
            
        }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
                return _figures[square.x, square.y];
            return Figure.None;
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
                _figures[square.x, square.y] = figure;
        }

        public Board Move(FigureMoving figureMoving)
        {
            Board next = new Board(Fen);
            next.SetFigureAt(figureMoving.From, Figure.None);
            next.SetFigureAt(figureMoving.To, figureMoving.Figure);
            
            Eat(next, figureMoving.To);

            if (MoveColor == Color.White)
                next.MoveNumber++;
            
            next.MoveColor = MoveColor.FlipColor();
            next.GenerateFen();
            return next;
        }

        private void Eat(Board next, Square to)
        {
            Eat(next, to, 1, 0);
            Eat(next, to, -1, 0);
            Eat(next, to, 0, 1);
            Eat(next, to, 0, -1);
        }

        private void Eat(Board next, Square to, int xOffset, int yOffset)
        {
            Square neighbour = new Square(to.x + xOffset, to.y + yOffset);
            Console.WriteLine($"{neighbour.x} {neighbour.y} - {neighbour.OnBoard()}");
            if (neighbour.OnBoard() && GetFigureAt(neighbour).GetColor() != MoveColor)
            {
                Square nextNeighbour = new Square(neighbour.x + xOffset, neighbour.y + yOffset);
                if (nextNeighbour.OnBoard() && GetFigureAt(nextNeighbour).GetColor() == MoveColor)
                {
                    next.SetFigureAt(neighbour, Figure.None);
                }
            }
        }
        
        private void InitFigures(string data)
        {
            string[] lines = data.Split('/');
            for (int y = Game.size - 1; y >= 0; y--)
            {
                for (int x = 0; x < Game.size; x++)
                {
                    _figures[x, y] = (Figure)lines[Game.size - 1 - y][x];
                }
            }
        }

        private void GenerateFen()
        {
            Fen = FenFigures() + " " + (MoveColor == Color.Black ? "b" : "w") + 
                  " " + MoveNumber;
        }

        private string FenFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = Game.size - 1; y >= 0; y--)
            {
                for (int x = 0; x < Game.size; x++)
                {
                    sb.Append(_figures[x, y] == Figure.None ? '.' : (char)_figures[x, y]);
                }

                if (y > 0)
                    sb.Append('/');
            }

            return sb.ToString();
        }
    }
}