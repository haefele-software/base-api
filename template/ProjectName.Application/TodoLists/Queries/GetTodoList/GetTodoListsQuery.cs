using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Models.Dtos;
using Sieve.Services;

namespace ProjectName.Application.TodoLists.Queries.GetTodoLists
{
    public class GetTodoListsQuery : IRequest<TodoListDto[]>
    {
        public long ListId { get; set; }
    }

    public class GetTodoListsQueryHandler : QueryRequestHandlerBase, IRequestHandler<GetTodoListsQuery, TodoListDto[]>
    {
        public GetTodoListsQueryHandler(IApplicationDbContext context, ILogger<GetTodoListsQueryHandler> logger, IMapper mapper, ISieveProcessor paginationProcessor)
            : base(context, logger, mapper, paginationProcessor)
        {
        }

        public async Task<TodoListDto[]> Handle(GetTodoListsQuery request, CancellationToken cancellationToken)
        {
            var lists = Context.TodoLists.Include(l => l.Items).Where(l => l.State == Domain.Enums.DataState.Active).AsNoTracking(); // Makes read-only queries faster 

            return await lists.ProjectTo<TodoListDto>(Mapper.ConfigurationProvider).ToArrayAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
