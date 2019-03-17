using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiStep.Data;
using MultiStep.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MultiStep.Pages.Register
{
    public class BasicModel : PageModel
    {
        private readonly MultiStepContext _context;

        public BasicModel(MultiStepContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BasicViewModel BasicViewModel { get; set; } = new BasicViewModel();

        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            BasicViewModel.Id = id;

            if (id > 0)
            {
                var registration = await _context.Registration.FindAsync(id);
                BasicViewModel.Name = registration.Name;
                BasicViewModel.IsComing = registration.IsComing;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var registration = new Registration();

            if (BasicViewModel.Id == 0)
            {
                _context.Registration.Add(registration);
            }
            else
            {
                registration = await _context.Registration.FindAsync(BasicViewModel.Id);
            }

            registration.Name = BasicViewModel.Name;
            registration.IsComing = BasicViewModel.IsComing;
            registration.StepCompleted = 1;

            await _context.SaveChangesAsync();

            if (registration.IsComing)
            {
                return RedirectToPage("./Attending", new { id = registration.Id });
            }
            else
            {
                return RedirectToPage("./Complete", new { id = registration.Id });
            }
        }
    }

    public class BasicViewModel
    {
        public int? Id { get; set; }

        [Required, StringLength(40)]
        public string Name { get; set; }

        [Display(Name = "Are you coming?")]
        public bool IsComing { get; set; }
    }
}