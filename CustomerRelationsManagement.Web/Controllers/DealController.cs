using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Constants;
using Microsoft.AspNetCore.Authorization;
using CustomerRelationsManagement.Web.Contracts;
using AutoMapper;
using CustomerRelationsManagement.Web.Models;
using CustomerRelationsManagement.Web.Repositories;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class DealController : Controller
    {
        private readonly IDealRepository dealRepository;
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public DealController(IDealRepository dealRepository
            , IClientRepository clientRepository
            , IMapper mapper, ApplicationDbContext context)
        {

            this.clientRepository = clientRepository;
            this.dealRepository = dealRepository;
            this.mapper = mapper;
            this.context = context;
        }

        // GET: Deal
        public async Task<IActionResult> Index()
        {
            var model = await dealRepository.GetDealOverviewList();
            return View(model);
            //return View(mapper.Map<List<DealViewModel>>(await dealRepository.GetAllAsync()));
        }

        // GET: Deal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await dealRepository.GetDeal(id);
            return View(model);
        }

        // GET: Deal/Create
        public IActionResult Create()
        {
            //List<Client> clients = await clientRepository.GetAllAsync();
            //ViewBag.Clients = clients;
            //ViewBag.Clients = clientRepository.GetAllAsync();

            var model = new DealCreateViewModel
            {
                Clients = new SelectList(context.Clients, "Id", "Name")
            };
            return View(model);
        }

        // POST: Deal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DealCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await dealRepository.CreateDeal(model);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error has occured please try again");
            }
            model.Clients = new SelectList(context.Clients, "Id", "Name", model.ClientId);
            return View(model);
        }

        // GET: Deal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await dealRepository.GetDeal(id);
            if (model == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = new SelectList(context.Clients, "Id", "Name", model.ClientId);
            return View(model);
        }

        // POST: Deal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DealViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var deal = mapper.Map<Deal>(model);
                    await dealRepository.UpdateAsync(deal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await dealRepository.Exists(model.Id))
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
            ViewData["ClientId"] = new SelectList(context.Clients, "Id", "Name", model.ClientId);
            return View(model);
        }

        // POST: Deal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await dealRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
