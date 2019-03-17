using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiStep.Data;
using System.Threading.Tasks;

namespace MultiStep.Pages.Register
{
    public class AttendingModel : PageModel
    {
        private readonly MultiStepContext _context;

        public AttendingModel(MultiStepContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AttendingViewModel AttendingViewModel { get; set; } = new AttendingViewModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AttendingViewModel.Id = id;

            var registration = await _context.Registration.FindAsync(id);
            AttendingViewModel.Ceremony = registration.Ceremony;
            AttendingViewModel.Reception = registration.Reception;
            AttendingViewModel.Trip = registration.Trip;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var registration = await _context.Registration.FindAsync(AttendingViewModel.Id);

            registration.Ceremony = AttendingViewModel.Ceremony;
            registration.Reception = AttendingViewModel.Reception;
            registration.Trip = AttendingViewModel.Trip;
            registration.StepCompleted = 2;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Meal", new { id = registration.Id });
        }
    }

    public class AttendingViewModel
    {
        public int Id { get; set; }
        public bool Ceremony { get; set; }
        public bool Reception { get; set; }
        public bool Trip { get; set; }
    }
}