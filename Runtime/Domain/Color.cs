namespace Uno.Runtime.Domain
{
    public enum Color
    {
        Blue,
        Red,
        Green,
        Yellow,
        Any
    }

    public static class ColorExtensions
    {
        public static bool Matches(this Color color, Color other)
        {
            return color == Color.Any ||
                   other == Color.Any ||
                   color == other;
        }
    }
}