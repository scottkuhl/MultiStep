using System.ComponentModel.DataAnnotations;

namespace MultiStep.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required, StringLength(40)]
        public string Name { get; set; }

        [Display(Name = "Coming?")]
        public bool IsComing { get; set; }

        [Display(Name = "Attending the Ceremony?")]
        public bool Ceremony { get; set; }

        [Display(Name = "Attending the Reception?")]
        public bool Reception { get; set; }

        [Display(Name = "Attending the Trip?")]
        public bool Trip { get; set; }

        public MealType Meal { get; set; }

        public int StepCompleted { get; set; }
    }

    public enum MealType
    {
        Vegetarian,
        Chicken,
        Fish
    }
}
