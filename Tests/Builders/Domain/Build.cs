namespace Uno.Tests.Builders.Domain
{
    public static class Build
    {
        public static TurnBuilder Turn() => TurnBuilder.New();
        public static PlayerBuilder Player() => PlayerBuilder.New();
    }
}