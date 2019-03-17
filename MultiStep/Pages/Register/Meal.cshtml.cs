using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiStep.Data;
using MultiStep.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiStep.Pages.Register
{
    public class MealModel : PageModel
    {
        private readonly MultiStepContext _context;

        public MealModel(MultiStepContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MealViewModel MealViewModel { get; set; } = new MealViewModel();

        public List<SelectListItem> MealList { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MealViewModel.Id = id;

            var registration = await _context.Registration.FindAsync(id);
            MealViewModel.Meal = registration.Meal;

            foreach(var meal in Enum.GetValues(typeof(MealType)))
            {
                MealList.Add(new SelectListItem { Text = Enum.GetName(typeof(MealType), meal), Value = meal.ToString() });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var registration = await _context.Registration.FindAsync(MealViewModel.Id);

            registration.Meal = MealViewModel.Meal;
            registration.StepCompleted = 3;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Complete", new { id = registration.Id });
        }
    }

    public class MealViewModel
    {
        public int Id { get; set; }
        public MealType Meal { get; set; }
    }
}