using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Contracts;
using AutoMapper;
using CustomerRelationsManagement.Web.Models;
using CustomerRelationsManagement.Web.Repositories;

namespace CustomerRelationsManagement.Web.Controllers
{
    public class PositionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPositionRepository positionRepository;
        private readonly IMapper mapper;

        public PositionsController(ApplicationDbContext context, IPositionRepository positionRepository, IMapper mapper)
        {
            _context = context;
            this.positionRepository = positionRepository;
            this.mapper = mapper;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            var model = mapper.Map<List<PositionViewModel>>(await positionRepository.GetAllAsync());
            return View(model);
            
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var model = mapper.Map<PositionViewModel>(await positionRepository.GetAsync(id));
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionViewModel model)
        {
            if (ModelState.IsValid)
            {
                await positionRepository.AddAsync(mapper.Map<Position>(model));
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var model = mapper.Map<PositionViewModel>(await positionRepository.GetAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PositionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await positionRepository.UpdateAsync(mapper.Map<Position>(model));
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error has occured please try again");
            }
            return View(model);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Positions'  is null.");
            }
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
