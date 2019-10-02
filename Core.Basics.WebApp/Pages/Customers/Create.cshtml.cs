using System.Collections.Generic;
using Core.Basics.IServices;
using Core.Basics.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core.Basics.WebApp.Pages.Customers
{
    public class CreateCustomers : PageModel {

        private readonly ICustomersService _customersService;
        public CreateCustomers(ICustomersService customersService) {
            _customersService = customersService;
        }
        
        public IActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            await _customersService.AddAsync(Customer);
            return RedirectToPage("./Index");
        }
        [BindProperty]
        public Customer Customer {get; set;}
    }
}