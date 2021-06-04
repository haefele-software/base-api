using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using Sieve.Services;

namespace ProjectName.Application
{
    public abstract class QueryRequestHandlerBase : RequestHandlerBase
    {
        protected readonly ISieveProcessor PaginationProcessor;

        public QueryRequestHandlerBase(IApplicationDbContext context, ILogger logger, IMapper mapper, ISieveProcessor paginationProcessor) : base(context, logger, mapper)
        {
            PaginationProcessor = paginationProcessor;
        }
    }
}
