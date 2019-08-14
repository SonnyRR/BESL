namespace BESL.Application.Exceptions
{
    public class InvalidMatchParticipantsCountException : BaseCustomException
    {
        public InvalidMatchParticipantsCountException(int expectedPlayers)
            : base($"Participated players count must be equal to {expectedPlayers}")
        {
        }
    }
}
