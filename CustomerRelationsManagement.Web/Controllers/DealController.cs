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

        public DealController(IDealRepository dealRepository
            , IClientRepository clientRepository
            , IMapper mapper)
        {

            this.clientRepository = clientRepository;
            this.dealRepository = dealRepository;
            this.mapper = mapper;
        }

        // GET: Deal
        public async Task<IActionResult> Index()
        {
            var deals = await dealRepository.GetAllAsync();
            var dealViewModels = mapper.Map<List<DealViewModel>>(deals);
            return View(dealViewModels);
            //return View(mapper.Map<List<DealViewModel>>(await dealRepository.GetAllAsync()));
        }

        // GET: Deal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var deal = await dealRepository.GetAsync(id);

            if (deal == null)
            {
                return NotFound();
            }

            var dealViewModel = mapper.Map<DealViewModel>(deal);
            return View(dealViewModel);
        }

        // GET: Deal/Create
        public async Task<IActionResult> Create()
        {
            //List<Client> clients = await clientRepository.GetAllAsync();
            //ViewBag.Clients = clients;
            //ViewBag.Clients = clientRepository.GetAllAsync();
            return View();
        }

        // POST: Deal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DealViewModel dealViewModel)
        {
            if (ModelState.IsValid)
            {
                var deal = mapper.Map<Deal>(dealViewModel);
                await dealRepository.AddAsync(deal);
                return RedirectToAction(nameof(Index));
            }
            return View(dealViewModel);
        }

        // GET: Deal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var deal = await dealRepository.GetAsync(id);
            if (deal == null)
            {
                return NotFound();
            }

            var dealViewModel = mapper.Map<DealViewModel>(deal);
            return View(dealViewModel);
        }

        // POST: Deal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DealViewModel dealViewModel)
        {
            if (id != dealViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var deal = mapper.Map<Deal>(dealViewModel);
                    await dealRepository.UpdateAsync(deal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await dealRepository.Exists(dealViewModel.Id))
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
            return View(dealViewModel);
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
