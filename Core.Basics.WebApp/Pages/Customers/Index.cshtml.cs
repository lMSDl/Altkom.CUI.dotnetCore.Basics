using System.Collections.Generic;
using Core.Basics.IServices;
using Core.Basics.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Core.Basics.WebApp.Pages.Customers
{
    public class IndexCustomers : PageModel {

        private readonly ICustomersService _customersService;
        public IndexCustomers(ICustomersService customersService) {
            _customersService = customersService;
        }
        public IList<Customer> Customers {get; set;} = new List<Customer>();

        public async Task OnGetAsync() {
            Customers = (await _customersService.GetAsync()).ToList();
        }
    }
}