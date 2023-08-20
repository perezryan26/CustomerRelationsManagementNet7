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
using Microsoft.AspNetCore.Authorization;
using CustomerRelationsManagement.Web.Constants;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class PositionsController : Controller
    {
        private readonly IPositionRepository positionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PositionsController> logger;

        public PositionsController(IPositionRepository positionRepository, IMapper mapper, ILogger<PositionsController> logger)
        {
            this.positionRepository = positionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = mapper.Map<List<PositionViewModel>>(await positionRepository.GetAllAsync());
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving positions.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving positions.");
            }

        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Position not found.");
            }

            try
            {
                var model = mapper.Map<PositionViewModel>(await positionRepository.GetAsync(id));

                if (model == null)
                {
                    return NotFound("Position not found.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while viewing details for the position.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while viewing details for the position.");
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    await positionRepository.AddAsync(mapper.Map<Position>(model));

                    TempData["SuccessMessage"] = "Position created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the position.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the position.");
            }
            return View(model);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = mapper.Map<PositionViewModel>(await positionRepository.GetAsync(id));
            
            if (model == null)
            {
                return NotFound("Position not found.");
            }
            return View(model);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PositionViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("IDs do not match.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await positionRepository.UpdateAsync(mapper.Map<Position>(model));

                    TempData["SuccessMessage"] = "Position updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await positionRepository.Exists(model.Id))
                    {
                        return NotFound("Position not found.");
                    }
                    else
                    {
                        return Conflict("Concurrency conflict. The position has been modified by another user.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while saving the position changes.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the position changes.");
                }
            }
            return View(model);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var position = await positionRepository.GetAsync(id);

                if (position == null)
                {
                    return NotFound("Task not found.");
                }

                await positionRepository.DeleteAsync(id);

                TempData["SuccessMessage"] = "Position deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting the position.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the position.");
            }
        }
    }
}
