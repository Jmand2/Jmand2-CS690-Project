using System;
using System.Collections.Generic;
using System.Linq;

namespace JakeFitnessApp
{
    public class MealPlan
    {
        public List<Meal> Meals { get; set; } = new List<Meal>();

        public void Generate(UserPreferences prefs)
        {
            Meals.Clear();
            Random rand = new Random();
            int mealsPerDay = 3;
            int days = 7;
            int targetPerMeal = prefs.TargetCalories / mealsPerDay;

            for (int day = 0; day < days; day++)
            {
                Console.WriteLine($"\nDay {day + 1}:");
                for (int meal = 0; meal < mealsPerDay; meal++)
                {
                    var options = Meal.AvailableMeals.Where(m => Math.Abs(m.Calories - targetPerMeal) < 150).ToList();
                    if (options.Count > 0)
                    {
                        var selectedMeal = options[rand.Next(options.Count)];
                        Meals.Add(selectedMeal);
                        Console.WriteLine($"- {selectedMeal.Name}");
                    }
                }
            }
        }

        public Dictionary<string, int> GenerateGroceryList()
        {
            var groceryDict = new Dictionary<string, int>();
            foreach (var meal in Meals)
            {
                foreach (var ingredient in meal.Ingredients)
                {
                    if (!groceryDict.ContainsKey(ingredient))
                        groceryDict[ingredient] = 0;
                    groceryDict[ingredient]++;
                }
            }
            return groceryDict;
        }
    }
}