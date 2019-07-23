namespace BESL.Application.Games.Commands.Validation
{
    public static class Constants
    {
        public const int NAME_MIN_LENGTH = 3, NAME_MAX_LENGTH = 40;
        public const int DESC_MIN_LENGTH = 160, DESC_MAX_LENGTH = 1000;

        public const string NAME_LENGTH_MSG = "Game name length must be between {0} and {1} characters long!";
        public const string DESC_LENGTH_MSG = "Game description length must be between {0} and {1} characters long!";
    }
}
