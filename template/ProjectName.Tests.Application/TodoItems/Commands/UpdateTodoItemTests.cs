using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.TodoItems.Commands.CreateTodoItem;
using ProjectName.Application.TodoItems.Commands.UpdateTodoItem;
using ProjectName.Application.TodoLists.Commands.CreateTodoList;
using ProjectName.Common.Exceptions;
using static ProjectName.Application.Integration.Tests.Testing;

namespace ProjectName.Application.Integration.Tests.TodoItems.Commands
{
    public class UpdateTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateTodoItemCommand
            {
                ReferenceId = Guid.NewGuid(), //"some rando guid"
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var listDto = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            }).ConfigureAwait(false);

            var itemDto = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listDto.Id,
                Title = "New Item"
            }).ConfigureAwait(false);

            var command = new UpdateTodoItemCommand
            {
                ReferenceId = itemDto.ReferenceId,
                Title = "Updated Item Title"
            };

            await SendAsync(command).ConfigureAwait(false);

            var item = await FindAsync<TodoItem>(itemDto.Id).ConfigureAwait(false);

            item.Title.Value.Should().Be(command.Title);
        }
    }
}
