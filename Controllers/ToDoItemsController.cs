﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoable.Data;
using ToDoable.Models;

namespace ToDoable.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ToDoableDbContext _context;

        public ToDoItemsController(ToDoableDbContext context)
        {
            _context = context;
        }

        // GET: ToDoItems
        public async Task<IActionResult> Index()
        {
            var toDoableDbContext = _context.TodoItems.Include(t => t.Category).Include(t => t.ToDoableUser);
            return View(await toDoableDbContext.ToListAsync());
        }

        // GET: ToDoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.TodoItems
                .Include(t => t.Category)
                .Include(t => t.ToDoableUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["ToDoableUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsCompleted,DueDate,CategoryId,ToDoableUserId")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", toDoItem.CategoryId);
            ViewData["ToDoableUserId"] = new SelectList(_context.Users, "Id", "Id", toDoItem.ToDoableUserId);
            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.TodoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", toDoItem.CategoryId);
            ViewData["ToDoableUserId"] = new SelectList(_context.Users, "Id", "Id", toDoItem.ToDoableUserId);
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsCompleted,DueDate,CategoryId,ToDoableUserId")] ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", toDoItem.CategoryId);
            ViewData["ToDoableUserId"] = new SelectList(_context.Users, "Id", "Id", toDoItem.ToDoableUserId);
            return View(toDoItem);
        }

        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.TodoItems
                .Include(t => t.Category)
                .Include(t => t.ToDoableUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoItem = await _context.TodoItems.FindAsync(id);
            if (toDoItem != null)
            {
                _context.TodoItems.Remove(toDoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
