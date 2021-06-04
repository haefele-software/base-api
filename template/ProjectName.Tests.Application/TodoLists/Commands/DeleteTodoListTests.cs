using FluentAssertions;
using NUnit.Framework;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.TodoLists.Commands.CreateTodoList;
using ProjectName.Application.TodoLists.Commands.DeleteTodoList;
using ProjectName.Common.Exceptions;
using System;
using System.Threading.Tasks;
using static ProjectName.Application.Integration.Tests.Testing;

namespace ProjectName.Application.Integration.Tests.TodoLists.Commands
{
    public class DeleteTodoListTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoListId()
        {
            var command = new DeleteTodoListCommand { ReferenceId = Guid.NewGuid() };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoList()
        {
            var listDto = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            }).ConfigureAwait(false);

            await SendAsync(new DeleteTodoListCommand
            {
                ReferenceId = listDto.ReferenceId
            }).ConfigureAwait(false);

            var list = await FindAsync<TodoList>(listDto.Id).ConfigureAwait(false);

            list.Should().BeNull();
        }
    }
}
