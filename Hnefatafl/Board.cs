using System.Text;

namespace Hnefatafl
{
    public class Board
    {
        private readonly Figure[,] _figures;
        
        public string Fen { get; private set; }
        public Color MoveColor { get; private set; }
        public int MoveNumber { get; private set; }
        
        public Board(string fen)
        {
            Fen = fen;
            _figures = new Figure[Game.Size, Game.Size];
            Init();
        }

        private void Init()
        {
            var parts = Fen.Split();
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
        
        private Square GetKingSquare() {
            for (var y = Game.Size - 1; y >= 0; y--)
            {
                for (var x = 0; x < Game.Size; x++)
                {
                    if (_figures[x, y] == Figure.King)
                        return new Square(x, y);
                }
            }

            return Square.none;
        }

        public Board Move(FigureMoving figureMoving)
        {
            var next = new Board(Fen);
            next.SetFigureAt(figureMoving.From, Figure.None);
            next.SetFigureAt(figureMoving.To, figureMoving.Figure);
            
            Eat(next, figureMoving.To);

            if (MoveColor == Color.White)
                next.MoveNumber++;
            
            next.MoveColor = MoveColor.FlipColor();
            next.GenerateFen();
            return next;
        }

        public Color CheckWonColor()
        {
            var kingSquare = GetKingSquare();
            bool isKingSurrounded = GetFigureAt(new Square(kingSquare.x + 1, kingSquare.y)).GetColor() == Color.Black &&
                                    GetFigureAt(new Square(kingSquare.x - 1, kingSquare.y)).GetColor() == Color.Black &&
                                    GetFigureAt(new Square(kingSquare.x, kingSquare.y + 1)).GetColor() == Color.Black &&
                                    GetFigureAt(new Square(kingSquare.x, kingSquare.y - 1)).GetColor() == Color.Black;
            bool isKingOnCorner = kingSquare.IsСorner();
            if (isKingSurrounded)
            {
                SetFigureAt(kingSquare, Figure.None);
                return Color.Black;
            }
            if (isKingOnCorner)
            {
                return Color.White;
            }

            return Color.None;
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
            var neighbour = new Square(to.x + xOffset, to.y + yOffset);
            var neighbourFigure = GetFigureAt(neighbour);
            if (neighbour.OnBoard() && neighbourFigure.GetColor() != MoveColor)
            {
                if(neighbourFigure == Figure.King) return;
                var nextNeighbour = new Square(neighbour.x + xOffset, neighbour.y + yOffset);
                if (nextNeighbour.OnBoard() && GetFigureAt(nextNeighbour).GetColor() == MoveColor)
                {
                    next.SetFigureAt(neighbour, Figure.None);
                }
            }
        }

        private void InitFigures(string data)
        {
            var lines = data.Split('/');
            for (var y = Game.Size - 1; y >= 0; y--)
            {
                for (var x = 0; x < Game.Size; x++)
                {
                    _figures[x, y] = (Figure)lines[Game.Size - 1 - y][x];
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
            var sb = new StringBuilder();
            for (var y = Game.Size - 1; y >= 0; y--)
            {
                for (var x = 0; x < Game.Size; x++)
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