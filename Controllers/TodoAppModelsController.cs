using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoAppRil.Data;
using TodoAppRil.Models;

namespace TodoAppRil.Controllers
{
    public class TodoAppModelsController : Controller
    {
        private readonly TodoAppRilContext _context;

        public TodoAppModelsController(TodoAppRilContext context)
        {
            _context = context;
        }

        // GET: TodoAppModels
        public async Task<IActionResult> Index(string category, int? priority)
        {
            // Get distinct categories and priorities for dropdowns
            var categories = await _context.TodoAppModel
                .Select(t => t.Category)
                .Distinct()
                .ToListAsync();

            var priorities = await _context.TodoAppModel
                .Select(t => t.Priority)
                .Distinct()
                .OrderBy(p => p)
                .ToListAsync();

            // Query for filtering
            var todos = _context.TodoAppModel.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                todos = todos.Where(t => t.Category == category);
            }
            if (priority.HasValue)
            {
                todos = todos.Where(t => t.Priority == priority.Value);
            }

            ViewData["Categories"] = new SelectList(categories);
            ViewData["Priorities"] = new SelectList(priorities);

            return View(await todos.ToListAsync());

            //return View(await _context.TodoAppModel.ToListAsync());
        }

        // GET: TodoAppModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoAppModel = await _context.TodoAppModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoAppModel == null)
            {
                return NotFound();
            }

            return View(todoAppModel);
        }

        // GET: TodoAppModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoAppModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Category,Priority,DueDate")] TodoAppModel todoAppModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoAppModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoAppModel);
        }

        // GET: TodoAppModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoAppModel = await _context.TodoAppModel.FindAsync(id);
            if (todoAppModel == null)
            {
                return NotFound();
            }
            return View(todoAppModel);
        }

        // POST: TodoAppModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Category,Priority,DueDate")] TodoAppModel todoAppModel)
        {
            if (id != todoAppModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoAppModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoAppModelExists(todoAppModel.Id))
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
            return View(todoAppModel);
        }

        // GET: TodoAppModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoAppModel = await _context.TodoAppModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoAppModel == null)
            {
                return NotFound();
            }

            return View(todoAppModel);
        }

        // POST: TodoAppModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoAppModel = await _context.TodoAppModel.FindAsync(id);
            if (todoAppModel != null)
            {
                _context.TodoAppModel.Remove(todoAppModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoAppModelExists(int id)
        {
            return _context.TodoAppModel.Any(e => e.Id == id);
        }
    }
}
