using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Models.Dtos;
using ProjectName.Application.Models.Mappings;
using ProjectName.Application.Models.Pagination;
using Sieve.Services;

namespace ProjectName.Application.TodoItems.Queries.GetTodoItemsWithPagination
{
    public class GetTodoItemsWithPaginationQuery : PaginationRequest, IRequest<PaginatedList<TodoItemDto>>
    {
        public long ListId { get; set; }
    }

    public class GetTodoItemsWithPaginationQueryHandler : QueryRequestHandlerBase, IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemDto>>
    {
        private readonly IMapper _mapper;

        public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, ILogger<GetTodoItemsWithPaginationQueryHandler> logger, IMapper mapper, ISieveProcessor paginationProcessor) : base(context, logger, mapper, paginationProcessor)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedList<TodoItemDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var items = Context.TodoItems.AsNoTracking(); // Makes read-only queries faster 

            return await PaginationProcessor.Apply(request, items)
                .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page.GetValueOrDefault(), request.PageSize.GetValueOrDefault()).ConfigureAwait(false);
        }
    }
}
