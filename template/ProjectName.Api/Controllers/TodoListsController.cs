using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Application.Models.Dtos;
using ProjectName.Application.TodoLists.Commands.CreateTodoList;
using ProjectName.Application.TodoLists.Commands.DeleteTodoList;
using ProjectName.Application.TodoLists.Commands.UpdateTodoList;
using ProjectName.Application.TodoLists.Queries.GetTodoLists;
using System;
using System.Threading.Tasks;

namespace ProjectName.Api.Controllers
{
    [Authorize]
    [Route("v{version:apiVersion}/todolists")]
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet]
        [Authorize("read:todolists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoListDto[]>> Get()
        {
            return await Mediator.Send(new GetTodoListsQuery()).ConfigureAwait(false);
        }


        [HttpPost]
        [Authorize("write:todolists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoListDto>> Create(CreateTodoListCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPut("{referenceId}")]
        [Authorize("write:todolists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoListDto>> Update(Guid referenceId, UpdateTodoListCommand command)
        {
            if (referenceId != command.ReferenceId)
            {
                return BadRequest();
            }

            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpDelete("{referencId}")]
        [Authorize("write:todolists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid referenceId)
        {
            await Mediator.Send(new DeleteTodoListCommand { ReferenceId = referenceId }).ConfigureAwait(false);

            return NoContent();
        }
    }
}
