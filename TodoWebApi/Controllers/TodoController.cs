using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoWebApi.Data;
using TodoWebApi.Helpers;

namespace TodoWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public TodoDbContext dbContext;
        public TodoController(TodoDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Fetch the list of todoitem.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTodoItems()
        {
            var userId = this.GetUserId();
            var todoItems = this.dbContext.TodoItem.Where(x => x.UserId.Equals(userId)).ToList().OrderByDescending(x => x.CreatedDate);
            return Ok(todoItems);
        }

        /// <summary>
        /// Fetch a specific todoitem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTodoItem(Guid id)
        {
            var userId = this.GetUserId();
            var todoItems = this.dbContext.TodoItem.Where(x => x.Id.Equals(id) && x.UserId.Equals(userId)).FirstOrDefault();
            return Ok(todoItems);
        }

        /// <summary>
        /// Create a todoitem.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostTodoItem(TodoWebApi.Models.ViewModels.CreateTodoItem todoItem)
        {
            var userId = this.GetUserId();
            var addTodoItem = this.dbContext.TodoItem.Add(new Models.TodoItem 
            {
                UserId = userId,
                Title = todoItem.Title,
                Description = todoItem.Description,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                Status = false,
            });
            var result = this.dbContext.SaveChanges();
            return Ok(result);
        }

        /// <summary>
        /// Update a specific todoitem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTodoItem(Guid id,TodoWebApi.Models.ViewModels.CreateTodoItem todoItem)
        {
            var userId = this.GetUserId();

            var todoItemToUpdate = this.dbContext.TodoItem.Where(x => x.Id.Equals(id) && x.UserId.Equals(userId)).FirstOrDefault();
            if (todoItemToUpdate == null) 
            {
                return NoContent();
            }
            todoItemToUpdate.LastUpdatedDate = DateTime.UtcNow;
            todoItemToUpdate.Title = todoItem.Title;
            todoItemToUpdate.Description = todoItem.Description;

            var updateTodoItem = this.dbContext.TodoItem.Update(todoItemToUpdate);
            var result = this.dbContext.SaveChanges();

            return Ok(result);
        }

        /// <summary>
        /// Deletes a specific todoitem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTodoItem(Guid id)
        {
            var userId = this.GetUserId();
            var todoItemToDelete = this.dbContext.TodoItem.Where(x => x.Id.Equals(id) && x.UserId.Equals(userId)).FirstOrDefault();
            if (todoItemToDelete == null)
            {
                return NoContent();
            }

            var updateTodoItem = this.dbContext.TodoItem.Remove(todoItemToDelete);
            var result = this.dbContext.SaveChanges();

            return Ok(result);
        }

        private string GetUserId()
        {
            return User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
        }
    }
}
