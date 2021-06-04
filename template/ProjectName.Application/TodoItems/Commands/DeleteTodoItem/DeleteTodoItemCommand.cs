using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Entities;
using ProjectName.Common.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public Guid ReferenceId { get; set; }
    }

    public class DeleteTodoItemCommandHandler : RequestHandlerBase, IRequestHandler<DeleteTodoItemCommand>
    {
        public DeleteTodoItemCommandHandler(IApplicationDbContext context, ILogger<DeleteTodoItemCommandHandler> logger, IMapper mapper) : base(context, logger, mapper)
        { }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await Context.TodoItems.FirstOrDefaultAsync(x => x.ReferenceId.Value == request.ReferenceId, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.ReferenceId);
            }

            Context.TodoItems.Remove(entity);

            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
