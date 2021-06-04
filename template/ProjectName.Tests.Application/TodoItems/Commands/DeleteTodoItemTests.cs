using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.TodoItems.Commands.CreateTodoItem;
using ProjectName.Application.TodoItems.Commands.DeleteTodoItem;
using ProjectName.Application.TodoLists.Commands.CreateTodoList;
using static ProjectName.Application.Integration.Tests.Testing;

namespace ProjectName.Application.Integration.Tests.TodoItems.Commands
{
    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public async Task ShouldDeleteTodoItem()
        {
            var list = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            }).ConfigureAwait(false);

            var itemDto = await SendAsync(new CreateTodoItemCommand
            {
                ListId = list.Id,
                Title = "New Item"
            }).ConfigureAwait(false);

            await SendAsync(new DeleteTodoItemCommand
            {
                ReferenceId = itemDto.ReferenceId
            }).ConfigureAwait(false);

            var item = await FindAsync<TodoItem>(itemDto.Id).ConfigureAwait(false);

            item.Should().BeNull();
        }
    }
}
