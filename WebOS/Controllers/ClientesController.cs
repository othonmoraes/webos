using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Models;
using PagedList;

namespace WebOS.Views
{
    public class ClientesController : Controller
    {
        private readonly WebOSContext _context;

        public ClientesController(WebOSContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)

        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;



            var clientes = from s in _context.Clientes
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.NomeRazaoSocial.Contains(searchString)
                    || s.Email.Contains(searchString)
                    || s.Telefone_fixo.Contains(searchString));
            }



            switch (sortOrder)
            {
                case "name_desc":
                    clientes = clientes.OrderByDescending(s => s.NomeRazaoSocial);
                    break;
                case "Date":
                    clientes = clientes.OrderBy(s => s.Telefone_fixo);
                    break;
                case "Date_desc":
                    clientes = clientes.OrderByDescending(s => s.Telefone_fixo);
                    break;
                default:
                    clientes = clientes.OrderBy(s => s.NomeRazaoSocial);
                    break;
            }

             int numero_paginas = 10;
            int pageSize = numero_paginas;
            int pageNumber = (page ?? 1);
            return View(clientes.OrderBy(n => n.NomeRazaoSocial).ToPagedList(pageNumber, pageSize));

           
        }

                     

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,NomeRazaoSocial,Telefone_fixo")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,NomeRazaoSocial,Telefone_fixo,Email")] Clientes clientes)
        {
            if (id != clientes.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(clientes.Codigo))
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
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Codigo == id);
        }
    }
}
