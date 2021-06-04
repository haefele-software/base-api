using Microsoft.Extensions.Options;
using ProjectName.Application.Domain.Entities;
using Sieve.Models;
using Sieve.Services;

namespace ProjectName.Application.Infrastructure
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(
            IOptions<SieveOptions> options)
            : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<TodoItem>(p => p.Title.Value)
                .HasName(nameof(TodoItem.Title).ToLowerInvariant())
                .CanFilter()
                .CanSort();

            mapper.Property<TodoItem>(p => p.Priority)
             .HasName(nameof(TodoItem.Priority).ToLowerInvariant())
             .CanSort();

            mapper.Property<TodoItem>(p => p.CreatedDate.Value)
             .HasName(nameof(TodoItem.CreatedDate).ToLowerInvariant())
             .CanSort()
             .CanFilter();

            mapper.Property<TodoItem>(p => p.TodoListId)
            .HasName(nameof(TodoItem.TodoListId).ToLowerInvariant())
            .CanSort()
            .CanFilter();

            return mapper;
        }
    }
}
