using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.TodoLists.Queries.GetTodoLists;
using static ProjectName.Application.Integration.Tests.Testing;

namespace ProjectName.Application.Integration.Tests.TodoLists.Queries
{
    public class GetTodoListsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnPriorityLevels()
        {
            var query = new GetTodoListsQuery();

            var result = await SendAsync(query).ConfigureAwait(false);

            result.Should().BeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllListsAndItems()
        {
            await AddAsync(new TodoList
            {
                Title = "Shopping",
                Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" }
                    }
            }).ConfigureAwait(false);

            var query = new GetTodoListsQuery();

            var result = await SendAsync(query).ConfigureAwait(false);

            result.Should().HaveCount(1);
            result.First().Items.Should().HaveCount(7);
        }
    }
}
