using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Application.Models.Dtos;
using ProjectName.Application.Models.Pagination;
using ProjectName.Application.TodoItems.Commands.CreateTodoItem;
using ProjectName.Application.TodoItems.Commands.DeleteTodoItem;
using ProjectName.Application.TodoItems.Commands.UpdateTodoItem;
using ProjectName.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace ProjectName.Api.Controllers
{
    [Authorize]
    [Route("v{version:apiVersion}/todoitems")]
    public class TodoItemsController : ApiControllerBase
    {
        /// <summary>
        /// Gets the todo items with pagination.
        /// 
        /// ?sorts=     Priority,Title,-created                 // sort by priority, then title, then descendingly by date created 
        /// &filters=   ListId>10, Title@=awesome title,        // filter to items with a list id of more than 10, and a title that contains the phrase "awesome title"
        /// &page=      1                                       // get the first page...
        /// &pageSize=  10                                      // ...which contains 10 posts
        /// 
        /// Supported Filter operators:
        /// 
        /// ==	Equals
        /// !=	Not equals
        /// >	Greater than
        /// <   Less than
        /// >=	Greater than or equal to
        /// <=	Less than or equal to
        /// @=	Contains
        /// _ = Starts with
        /// !@=	Does not Contains
        /// !_= Does not Starts with
        /// @=*	Case-insensitive string Contains
        /// _= *Case - insensitive string Starts with
        /// ==*	Case-insensitive string Equals
        /// !=*	Case-insensitive string Not equals
        /// !@=*	Case-insensitive string does not Contains
        /// !_= *Case - insensitive string does not Starts with
        /// 
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize("read:todoitems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the specified Todo Item.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize("create:todoitems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemDto>> Create(CreateTodoItemCommand command) => await Mediator.Send(command).ConfigureAwait(false);

        /// <summary>
        /// Updates the specified Todo Item.
        /// </summary>
        /// <param name="referenceId">The reference identifier.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPut("{referenceId}")]
        [Authorize("update:todoitems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemDto>> Update(Guid referenceId, UpdateTodoItemCommand command)
        {
            if (referenceId != command.ReferenceId)
            {
                return BadRequest();
            }

            return await Mediator.Send(command).ConfigureAwait(false);

        }

        /// <summary>
        /// Deletes the specified Todo Item.
        /// </summary>
        /// <param name="referencId">The referenc identifier.</param>
        /// <returns></returns>
        [HttpDelete("{referencId}")]
        [Authorize("delete:todoitems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid referenceId)
        {
            await Mediator.Send(new DeleteTodoItemCommand { ReferenceId = referenceId }).ConfigureAwait(false);

            return NoContent();
        }
    }
}
