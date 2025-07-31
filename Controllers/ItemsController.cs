using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MyappDatabaseContext _context;

        public ItemsController(MyappDatabaseContext context)
        {
            _context = context;
        }

        // GET: Items
        public IActionResult Index()
        {
            List<Item> items = new Item().GetAll(_context); // ดึงข้อมูลสินค้าทั้งหมดจากฐานข้อมูล 
            return View(items); // ส่งคืน ไปหน้า Index
        }

        // GET: Items/Detail
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = new Item().GetById(_context, id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name"); //เตรียม Dropdown 
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            Item im = new Item(); 
            im.SerialNumbers.Add(new SerialNumber());
            return View(im);
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                item.Create(_context);
                _context.SaveChanges(); 
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Items/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Item item = new Item().GetById(_context, id.Value);
           
            if (item == null)
            {
                return NotFound();
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name"); //เตรียม Dropdown
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    item.Update(_context);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            return View(item);
        }

        // GET: Items/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = _context.Items.Include(i => i.Category)
                                     .Include(s => s.SerialNumbers)
                                     .Include(ic => ic.ItemClient)
                                     .Include(c => c.Client)                             
                                     .FirstOrDefault(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Item item = new Item().GetById(_context, id);
            if (item != null)
            {
                item.Delete(_context);
            } 
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
