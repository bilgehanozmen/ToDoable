using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoable.Data;
using ToDoable.Models;

namespace ToDoable.Controllers
{
    public class ToDoItemController : Controller
    {
        private readonly ToDoableDbContext _context;

        public ToDoItemController(ToDoableDbContext context)
        {
            _context = context;
        }

        // GET: ToDoItem
        public async Task<IActionResult> Index()
        {
            var toDoableDbContext = _context.TodoItems.Include(t => t.Category).Include(t => t.ToDoableUser);
            return View(await toDoableDbContext.ToListAsync());
        }

        // GET: ToDoItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.TodoItems
                .Include(t => t.Category)
                .Include(t => t.ToDoableUser)
                .FirstOrDefaultAsync(m => m.ToDoItemId == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // GET: ToDoItem/Create
        public IActionResult AddOrEdit()
        {
            PopulateCategories();
           
            ViewData["ToDoableUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ToDoItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("ToDoItemId,Title,Description,IsCompleted,DueDate,CategoryId,ToDoableUserId")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(toDoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            if (toDoItem.ToDoItemId == 0)
                _context.TodoItems.Add(toDoItem);
            else
                _context.Update(toDoItem);

            ViewData["ToDoableUserId"] = new SelectList(_context.Users, "Id", "Id", toDoItem.ToDoableUserId);
            return View(toDoItem);
        }

        // GET: ToDoItem/Delete/5

        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Categories.ToList();
            Category DefaultCategory = new Category { Id = 0, Name = "Choose a category!" };
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
    }
}
