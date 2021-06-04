using AutoMapper;
using AutoMapper.QueryableExtensions;
using Destructurama.Attributed;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.Domain.Events.TodoItems;
using ProjectName.Application.Models.Dtos;
using ProjectName.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest<TodoItemDto>
    {
        public Guid ReferenceId { get; set; }

        [LogMasked(ShowLast = 3)]
        public string Title { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateTodoItemCommandHandler : RequestHandlerBase, IRequestHandler<UpdateTodoItemCommand, TodoItemDto>
    {

        public UpdateTodoItemCommandHandler(IApplicationDbContext context, ILogger<UpdateTodoItemCommandHandler> logger, IMapper mapper) : base(context, logger, mapper)
        { }

        public async Task<TodoItemDto> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await Context.TodoItems.FirstOrDefaultAsync(x => x.ReferenceId.Value == request.ReferenceId, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.ReferenceId);
            }

            entity.Title = request.Title;
            entity.Done = request.Done;

            if (entity.Done == true)
            {
                entity.DomainEvents.Add(new TodoItemCompletedEvent(entity));
            }
            else
            {
                entity.DomainEvents.Add(new TodoItemUpdatedEvent(entity));
            }

            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new List<TodoItem>() { entity }.AsQueryable().ProjectTo<TodoItemDto>(Mapper.ConfigurationProvider).First();
        }
    }

    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
