using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Web.Data;
using AutoMapper;
using CustomerRelationsManagement.Web.Models;
using CustomerRelationsManagement.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using CustomerRelationsManagement.Web.Constants;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class ClientController : Controller
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;
        

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            return View(mapper.Map<List<ClientViewModel>>(await clientRepository.GetAllAsync()));
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var client = await clientRepository.GetAsync(id);
            
            if (client == null)
            {
                return NotFound();
            }

            var clientViewModel = mapper.Map<ClientViewModel>(client);
            return View(clientViewModel);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = mapper.Map<Client>(clientViewModel);
                await clientRepository.AddAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(clientViewModel);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var client = await clientRepository.GetAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            var clientViewModel = mapper.Map<ClientViewModel>(client);
            return View(clientViewModel);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientViewModel clientViewModel)
        {
            if (id != clientViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var client = mapper.Map<Client>(clientViewModel);
                    await clientRepository.UpdateAsync(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await clientRepository.Exists(clientViewModel.Id))
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
            return View(clientViewModel);
        } 

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await clientRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
