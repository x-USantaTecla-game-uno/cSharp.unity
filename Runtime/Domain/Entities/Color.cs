using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<Color> PlayableColors
        {
            get
            {
                var allColors = Enum.GetValues(typeof(Color)).OfType<Color>();
                var allColorsButAny = allColors.Where(color => color != Color.Any);

                return allColorsButAny;
            }
        }
    }
}