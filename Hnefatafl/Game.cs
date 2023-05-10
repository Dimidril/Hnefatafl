using System;

namespace Hnefatafl
{
    public class Game
    {
        public const int Size = 11;
        private const string StartFen = "...aaaaa.../" +
                                       ".....a...../" +
                                       ".........../" +
                                       "a....d....a/" +
                                       "a...ddd...a/" +
                                       "aa.ddDdd.aa/" +
                                       "a...ddd...a/" +
                                       "a....d....a/" +
                                       ".........../" +
                                       ".....a...../" +
                                       "...aaaaa... " +
                                       "b 1";
        public string Fen { get; private set; }
        public Board Board { get; private set;}
        
        private readonly Move _move;

        public Game(string fen = StartFen)
        {
            Fen = fen;
            Board = new Board(fen);
            _move = new Move(Board);
        }

        Game(Board board)
        {
            Board = board;
            Fen = board.Fen;
            _move = new Move(Board);
        }
        
        public Game Move(string move)
        {
            var figureMoving = new FigureMoving(move, Board);
            if (!this._move.CanMove(figureMoving))
                return this;
            var nextBoard = Board.Move(figureMoving);
            var nextGame = new Game(nextBoard);

            var wonColor = nextBoard.CheckWonColor();
            if (wonColor != Color.None)
            {
                Console.WriteLine(wonColor.ToString());
                return null;
            }
            return nextGame;
        }

        public char GetFigureAt(int x, int y)
        {
            var square = new Square(x, y);
            var figure = Board.GetFigureAt(square);
            return figure == Figure.None ? '.' : (char)figure;
        }
    }
}