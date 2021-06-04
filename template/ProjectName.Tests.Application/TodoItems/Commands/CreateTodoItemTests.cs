using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.TodoItems.Commands.CreateTodoItem;
using ProjectName.Application.TodoLists.Commands.CreateTodoList;
using static ProjectName.Application.Integration.Tests.Testing;

namespace ProjectName.Application.Integration.Tests.TodoItems.Commands
{
    public class CreateTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTodoItemCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTodoItem()
        {
            var list = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            }).ConfigureAwait(false);

            var command = new CreateTodoItemCommand
            {
                ListId = list.Id,
                Title = "Tasks"
            };

            var itemDto = await SendAsync(command).ConfigureAwait(false);

            var item = await FindAsync<TodoItem>(itemDto.Id).ConfigureAwait(false);

            item.Should().NotBeNull();
            item.TodoListId.Should().Be(command.ListId);
            item.Title.Value.Should().Be(command.Title);
            item.CreatedDate.Value.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
