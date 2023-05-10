namespace Hnefatafl
{
    public class Move
    {
        private FigureMoving _figureMoving;
        private readonly Board _board;

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
                   _board.GetFigureAt(_figureMoving.From) != Figure.None &&
                   _figureMoving.Figure.GetColor() == _board.MoveColor;
        }
        
        private bool CanMoveTo()
        {
            return _figureMoving.To.OnBoard() && 
                   _board.GetFigureAt(_figureMoving.To) == Figure.None;
        }
        
        private bool CanFigureMove()
        {
            switch (_figureMoving.Figure)
            {
                case Figure.Attacker:
                case Figure.Defender:
                    return !(_figureMoving.To.IsThrone() || _figureMoving.To.IsСorner()) && CanStandardMove();
                case Figure.King:
                    return CanStandardMove();
                default:
                    return false;
            }
        }
        private bool CanStandardMove()
        {
            return (_figureMoving.SignX == 0 || _figureMoving.SignY == 0) && CanStraightMove();
        }

        private bool CanStraightMove()
        {
            var at = _figureMoving.From;

            do
            {
                at = new Square(at.x + _figureMoving.SignX, at.y + _figureMoving.SignY);
                if (at == _figureMoving.To)
                    return true;
            } while (at.OnBoard()&&
                     _board.GetFigureAt(at) == Figure.None);
            return false;
        }
    }
}