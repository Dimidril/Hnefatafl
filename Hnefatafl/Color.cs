namespace Hnefatafl
{
    public enum Color
    {
        None,
        Black,
        White
    }

    static class ColorMethods
    {
        public static Color FlipColor(this Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }
    }
}