using Microsoft.AspNetCore.Mvc;
using LiteDB;

namespace todo_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly LiteDbService _liteDbService;

        public TodoController(LiteDbService liteDbService)
        {
            _liteDbService = liteDbService;
        }

        [HttpGet]
        public IActionResult GetTasks([FromQuery] string? state = "all")
        {
            var collection = _liteDbService.GetCollection<TodoItem>("tasks");
            var allTasks = collection.FindAll().Select(doc => new TodoItem
            {
                Id = doc.Id,
                Title = doc.Title,
                State = doc.State,
                Content = doc.Content,
                CreationDate = doc.CreationDate
            }).ToList();

            allTasks = state?.ToLower() switch
            {
                "created" => allTasks.Where(task => task.State == TodoState.Open || task.State == TodoState.InProgress).ToList(),
                "finished" => allTasks.Where(task => task.State == TodoState.Finished).ToList(),
                "all" or null => allTasks,
                _ => allTasks
            };

            return Ok(allTasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }
            
            var collection = _liteDbService.GetCollection<TodoItem>("tasks");
            var task = collection.FindById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] TodoItem newItem)
        {
            if (newItem == null)
            {
                return BadRequest("Invalid input.");
            }

            newItem.Id = Guid.NewGuid();
            newItem.CreationDate = DateTime.UtcNow;

            var collection = _liteDbService.GetCollection<TodoItem>("tasks");
            collection.Insert(newItem);

            return CreatedAtAction(nameof(GetTask), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(Guid id, [FromBody] TodoItem updatedItem)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var collection = _liteDbService.GetCollection<TodoItem>("tasks");
            var existingTask = collection.FindOne(task => task.Id == id);

            if (existingTask == null)
            {
                return NotFound();
            }

            updatedItem.Id = id;
            collection.Update(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var collection = _liteDbService.GetCollection<BsonDocument>("tasks");
            var task = collection.FindById(id);

            if (task == null)
            {
                return NotFound();
            }

            collection.Delete(id);
            return NoContent();
        }
    }
}
