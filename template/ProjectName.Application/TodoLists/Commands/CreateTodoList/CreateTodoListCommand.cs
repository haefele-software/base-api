using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.Models.Dtos;

namespace ProjectName.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommand : IRequest<TodoListDto>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandHandler : RequestHandlerBase, IRequestHandler<CreateTodoListCommand, TodoListDto>
    {
        public CreateTodoListCommandHandler(IApplicationDbContext context, ILogger<CreateTodoListCommandHandler> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public async Task<TodoListDto> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList
            {
                Title = request.Title
            };

            Context.TodoLists.Add(entity);

            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new List<TodoList>() { entity }.AsQueryable().ProjectTo<TodoListDto>(Mapper.ConfigurationProvider).First();
        }
    }

    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        public CreateTodoListCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        }
    }
}
