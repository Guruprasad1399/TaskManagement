using Microsoft.AspNetCore.Mvc;
using Task_Management_System.Models;
using Task_Management_System.Data;

namespace Task_Management_System.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor: This initializes the controller with the database context
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index (GET): Retrieves all tasks and displays them in the Index view
        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        // Create (GET): Returns the Create view to create a new task
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST): Processes the form submission for creating a new task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItem task)
        {
            // Checks if the model state is valid (all required fields are filled) and then adds the task to the database
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // Edit (GET): Retrieves a task by ID and returns the Edit view
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // Edit (POST): Processes the form submission for updating an existing task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // Delete (GET): Retrieves a task by ID and returns the Delete view
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // Delete (POST): Processes the actual deletion of the task
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.Find(id);
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
