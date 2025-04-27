using System.Collections.Generic;

namespace JakeFitnessApp
{
    public class Meal
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
        public List<string> Ingredients { get; set; }

        public static List<Meal> AvailableMeals = new List<Meal>
        {
            new Meal("Protein Shake", 200, 25, 10, 5, new List<string>{"Protein Powder", "Milk"}),
            new Meal("Chicken Salad", 400, 30, 20, 15, new List<string>{"Chicken Breast", "Lettuce", "Tomato"}),
            new Meal("Oatmeal", 300, 10, 50, 5, new List<string>{"Oats", "Milk"}),
            new Meal("Grilled Salmon", 500, 35, 10, 30, new List<string>{"Salmon", "Broccoli", "Olive Oil"})
        };

        public Meal(string name, int calories, int protein, int carbs, int fat, List<string> ingredients)
        {
            Name = name;
            Calories = calories;
            Protein = protein;
            Carbs = carbs;
            Fat = fat;
            Ingredients = ingredients;
        }
    }
}
