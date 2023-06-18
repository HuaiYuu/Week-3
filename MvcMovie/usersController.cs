using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie
{
    public class usersController : Controller
    {
        private readonly MvcMovieContext _context;

        public usersController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: users
        public async Task<IActionResult> Index()
        {
              return _context.user != null ? 
                          View(await _context.user.ToListAsync()) :
                          Problem("Entity set 'MvcMovieContext.user'  is null.");
        }

        public async Task<IActionResult> member()
        {
            if (TempData.ContainsKey("Status"))
            {
                ViewBag.Status = TempData["Status"];
            }

            return _context.user != null ?
                        View(await _context.user.ToListAsync()) :
                        Problem("Entity set 'MvcMovieContext.user'  is null.");    
        }

        // GET: users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.user == null)
            {
                return NotFound();
            }

            var user = await _context.user
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: users/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult home()
        {
            if (TempData.ContainsKey("Status"))
            {
                ViewBag.Status = TempData["Status"];
            }
            return View();
        }

      
        // POST: users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,email,password")] user user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex) 
                {
                    return BadRequest(ex.Message);
                }
               
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Login([Bind("id,email,password")] user user)
        {



            var info = _context.user.FirstOrDefault(u => u.email == user.email);

            if (info == null)
            {
                TempData["Status"] = $"帳號或密碼錯誤!";
                return RedirectToAction(nameof(home));
            }
            if(info.email == user.email && info.password == user.password)
            {
                    TempData["Status"] = $"Welcome, {info.email}!";
                return RedirectToAction(nameof(member));

            }    
           
            return RedirectToAction(nameof(home));

        }

        // GET: users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.user == null)
            {
                return NotFound();
            }

            var user = await _context.user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,email,password")] user user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userExists(user.id))
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
            return View(user);
        }

        // GET: users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.user == null)
            {
                return NotFound();
            }

            var user = await _context.user
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.user == null)
            {
                return Problem("Entity set 'MvcMovieContext.user'  is null.");
            }
            var user = await _context.user.FindAsync(id);
            if (user != null)
            {
                _context.user.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool userExists(int id)
        {
          return (_context.user?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
