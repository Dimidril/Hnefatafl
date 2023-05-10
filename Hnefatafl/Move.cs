using System;
using System.Diagnostics;

namespace Hnefatafl
{
    public class Move
    {
        private FigureMoving _figureMoving;
        private Board _board;

        public Move(Board board)
        {
            _board = board;
        }

        public bool CanMove(FigureMoving figureMoving)
        {
            _figureMoving = figureMoving;
            return
                CanMoveFrom() &&
                CanMoveTo() &&
                CanFigureMove();
        }
        
        private bool CanMoveFrom()
        {
            return _figureMoving.From.OnBoard() &&
                   _figureMoving.Figure.GetColor() == _board.MoveColor;
        }
        
        private bool CanMoveTo()
        {
            Console.WriteLine(_board.GetFigureAt(_figureMoving.To).ToString());
            return _figureMoving.To.OnBoard() &&
                   _board.GetFigureAt(_figureMoving.To) == Figure.None;
        }
        
        private bool CanFigureMove()
        {
            return true;
        }
    }
}