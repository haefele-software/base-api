using FluentAssertions;
using NUnit.Framework;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.TodoLists.Commands.CreateTodoList;
using ProjectName.Application.TodoLists.Commands.UpdateTodoList;
using ProjectName.Common.Exceptions;
using System;
using System.Threading.Tasks;
using static ProjectName.Application.Integration.Tests.Testing;

namespace ProjectName.Application.Integration.Tests.TodoLists.Commands
{
    public class UpdateTodoListTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoListId()
        {
            var command = new UpdateTodoListCommand
            {
                ReferenceId = Guid.NewGuid(),
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoList()
        {
            var listDto = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            }).ConfigureAwait(false);

            var command = new UpdateTodoListCommand
            {
                ReferenceId = listDto.ReferenceId,
                Title = "Updated List Title"
            };

            await SendAsync(command).ConfigureAwait(false);

            var list = await FindAsync<TodoList>(listDto.Id).ConfigureAwait(false);

            list.Title.Value.Should().Be(command.Title);
        }
    }
}
