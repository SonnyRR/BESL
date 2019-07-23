namespace BESL.Application.TournamentFormats.Commands.Validation
{
    public static class Constants
    {
        public const int NAME_MIN_LENGTH = 3, NAME_MAX_LENGTH = 25;
        public const int DESC_MIN_LENGTH = 25, DESC_MAX_LENGTH = 500;

        public const int TEAM_PLAYERS_MIN_COUNT = 1, TEAM_PLAYERS_MAX_COUNT = 12;

        public const string NAME_LENGTH_MSG = "Format name length must be between {0} and {1} characters long!";
        public const string DESC_LENGTH_MSG = "Format description length must be between {0} and {1} characters long!";

        public const string TEAM_PLAYERS_COUNT_MSG = "Team players count must be between {0} and {1}!";
    }
}
