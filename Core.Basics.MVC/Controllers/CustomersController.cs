using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Basics.MVC.Models;
using Core.Basics.IServices;
using Core.Basics.Models;

namespace Core.Basics.MVC.Controllers
{
    public class CustomersController : Controller
    {
        private ICustomersService _customersService;
        public CustomersController(ICustomersService customersService) {
            _customersService = customersService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _customersService.GetAsync());
        }

        public IActionResult Create()
        {
            return View(new Customer());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Gender,LoyaltyCardId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customersService.AddAsync(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _customersService.GetAsync(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Gender,LoyaltyCardId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customersService.UpdateAsync(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(Customer customer)
        {
            if(await _customersService.DeleteAsync(customer.Id))
                return RedirectToAction("Index");
            return View(customer.Id);
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _customersService.GetAsync(id));
        }
    }
}
