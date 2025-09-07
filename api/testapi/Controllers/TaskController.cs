using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testapi.Models;
using testapi.Data;
using testapi.DTOs;
using System.Linq;

namespace testapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTasks()
        {
            // Just get all TaskItem rows without including TaskTags/Tags
            var tasks = await _context.Data.ToListAsync();
            return Ok(tasks);
        }*/

        //  GET /api/tags/{id}/tasks: Get all tasks for a tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            var tasks = await _context.Data
            .Include(t => t.TaskTags)
            .ThenInclude(tt => tt.Tag)
            .Select(t => new TaskDto(
                t.ProdataID,
                t.Register,
                t.Diagnostic,
                t.Packaging,
                t.TaskTags.Select(tt => new TagDto(
                    tt.Tag.ProdataTagsID,
                    tt.Tag.ErrCode.ToString(),
                    tt.Tag.Station.ToString()
                )).ToList()
            ))
            .ToListAsync();

            return Ok(tasks);
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {

            var task = await _context.Data
                             .Include(t => t.Tags)
                             .FirstOrDefaultAsync(t => t.ProdataID == id);

            if (task == null)
                return NotFound();

            return task;
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem task)
        {
            foreach (var tag in task.Tags.ToList())
            {
                var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.ProdataTagsID == tag.ProdataTagsID);
                if (existingTag != null)
                {
                    // Replace with existing
                    task.Tags.Remove(tag);
                    task.Tags.Add(existingTag);
                }
            }

            _context.Data.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.ProdataID }, task);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem task)
        {
            if (id != task.ProdataID)
                return BadRequest();

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Data.Any(t => t.ProdataID == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/tasks/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Data.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Data.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}