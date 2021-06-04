using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.TodoLists.Commands.CreateTodoList;
using System.Threading.Tasks;
using static ProjectName.Application.Integration.Tests.Testing;

namespace ProjectName.Application.Integration.Tests.TodoLists.Commands
{
    public class CreateTodoListTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTodoListCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTodoList()
        {
            var command = new CreateTodoListCommand
            {
                Title = "Tasks"
            };

            var listDto = await SendAsync(command).ConfigureAwait(false);

            var list = await FindAsync<TodoList>(listDto.Id).ConfigureAwait(false);

            list.Should().NotBeNull();
            list.Title.Value.Should().Be(command.Title);
        }
    }
}
