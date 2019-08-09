namespace BESL.Application.Teams.Commands.Validation
{
    internal static class Constants
    {
        internal const int NAME_MIN_LENGTH = 3, NAME_MAX_LENGTH = 35;
        internal const int DESC_MIN_LENGTH = 20, DESC_MAX_LENGTH = 750;
        internal const string URL_EXPRESSION = @"(https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*))?";

        internal const string NAME_LENGTH_MSG = "Team name length must be between {0} and {1} characters long!";
        internal const string DESC_LENGTH_MSG = "Team description length must be between {0} and {1} characters long!";
        internal const string URL_EXPRESSION_MSG = "Homepage input value is not a valid URL!";
    }
}
