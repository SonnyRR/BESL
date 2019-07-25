namespace BESL.Application.Tournaments.Commands.Delete
{
    using System;
    using MediatR;

    public class DeleteTournamentCommand : IRequest
    {
        public int Id { get; set; }


    }
}
