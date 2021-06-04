using AutoMapper;
using AutoMapper.QueryableExtensions;
using Destructurama.Attributed;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.Domain.Events.TodoItems;
using ProjectName.Application.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<TodoItemDto>
    {
        public long ListId { get; set; }

        [LogMasked(ShowLast = 3)]
        public string Title { get; set; }
    }

    public class CreateTodoItemCommandHandler : RequestHandlerBase, IRequestHandler<CreateTodoItemCommand, TodoItemDto>
    {
        public CreateTodoItemCommandHandler(IApplicationDbContext context, ILogger<CreateTodoItemCommandHandler> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public async Task<TodoItemDto> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                TodoListId = request.ListId,
                Title = request.Title,
                Done = false
            };

            entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));

            Context.TodoItems.Add(entity);

            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new List<TodoItem>() { entity }.AsQueryable().ProjectTo<TodoItemDto>(Mapper.ConfigurationProvider).First();
        }
    }
}
