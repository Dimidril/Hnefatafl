using System;

namespace Hnefatafl
{
    public class Game
    {
        public const int size = 11;
        public const string startFen = "...aaaaa.../" +
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
        private Board Board;
        private Move move;

        public Game(string fen = startFen)
        {
            Fen = fen;
            Board = new Board(fen);
            move = new Move(Board);
        }

        Game(Board board)
        {
            Board = board;
            Fen = board.Fen;
            move = new Move(Board);
        }
        
        public Game Move(string move)
        {
            
            FigureMoving figureMoving = new FigureMoving(move);
            if (!this.move.CanMove(figureMoving))
                return this;
            Board nextBoard = Board.Move(figureMoving);
            Game nextGame = new Game(nextBoard);
            return nextGame;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure figure = Board.GetFigureAt(square);
            return figure == Figure.None ? '.' : (char)figure;
        }
    }
}