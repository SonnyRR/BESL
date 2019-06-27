namespace BESL.Application.Games.Commands.Modify
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper.Configuration;
    using BESL.Application.Interfaces;
    using MediatR;

    public class ModifyGameCommandHandler : IRequestHandler<ModifyGameCommand, bool>
    {
        private readonly IApplicationDbContext context;
        private readonly IConfiguration configuration;

        public ModifyGameCommandHandler(IApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public Task<bool> Handle(ModifyGameCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
