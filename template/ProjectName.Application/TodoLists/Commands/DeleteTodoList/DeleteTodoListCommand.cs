using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Entities;
using ProjectName.Common.Exceptions;

namespace ProjectName.Application.TodoLists.Commands.DeleteTodoList
{
    public class DeleteTodoListCommand : IRequest
    {
        public Guid ReferenceId { get; set; }
    }

    public class DeleteTodoListCommandHandler : RequestHandlerBase, IRequestHandler<DeleteTodoListCommand>
    {
        public DeleteTodoListCommandHandler(IApplicationDbContext context, ILogger<DeleteTodoListCommandHandler> logger, IMapper mapper) : base(context, logger, mapper)
        { }

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await Context.TodoLists.FirstOrDefaultAsync(x => x.ReferenceId.Value == request.ReferenceId, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.ReferenceId);
            }

            Context.TodoLists.Remove(entity);

            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
