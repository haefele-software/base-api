using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;

namespace ProjectName.Application
{
    public abstract class RequestHandlerBase
    {
        protected RequestHandlerBase(IApplicationDbContext context, ILogger logger, IMapper mapper)
        {
            Context = context;
            Logger = logger;
            Mapper = mapper;
        }

        protected readonly IApplicationDbContext Context;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
    }
}
