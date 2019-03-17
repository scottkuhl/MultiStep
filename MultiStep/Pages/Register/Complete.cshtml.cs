using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MultiStep.Data;
using MultiStep.Models;
using System.Threading.Tasks;

namespace MultiStep.Pages.Register
{
    public class CompleteModel : PageModel
    {
        private readonly MultiStepContext _context;

        public CompleteModel(MultiStepContext context)
        {
            _context = context;
        }

        public Registration Registration { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Registration = await _context.Registration.FirstOrDefaultAsync(m => m.Id == id);
            return Page();
        }
    }
}
