using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Destructurama.Attributed;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.Models.Dtos;
using ProjectName.Common.Exceptions;

namespace ProjectName.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest<TodoListDto>
    {
        public Guid ReferenceId { get; set; }

        [LogMasked(ShowLast = 3)]
        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : RequestHandlerBase, IRequestHandler<UpdateTodoListCommand, TodoListDto>
    {

        public UpdateTodoListCommandHandler(IApplicationDbContext context, ILogger<UpdateTodoListCommandHandler> logger, IMapper mapper) : base(context, logger, mapper)
        { }

        public async Task<TodoListDto> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await Context.TodoLists.FirstOrDefaultAsync(x => x.ReferenceId.Value == request.ReferenceId, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.ReferenceId);
            }

            entity.Title = request.Title;

            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new List<TodoList>() { entity }.AsQueryable().ProjectTo<TodoListDto>(Mapper.ConfigurationProvider).First();
        }
    }
    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        public UpdateTodoListCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
