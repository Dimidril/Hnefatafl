namespace Hnefatafl
{
    public enum Figure
    {
        None = '.',
        Attacker = 'a',
        Defender = 'd',
        King = 'D'
    }
    
    static class FigureMethods{
        public static Color GetColor(this Figure figure)
        {
            switch (figure)
            {
                case Figure.Attacker:
                    return Color.Black;
                case Figure.Defender:
                case Figure.King:
                    return Color.White;
                default:
                    return Color.None;
            }
        }
    }
}