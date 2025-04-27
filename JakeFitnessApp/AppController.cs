using System;
using System.Collections.Generic;
using System.Linq;

namespace JakeFitnessApp
{
    public class AppController
    {
        private UserPreferences preferences = new UserPreferences();
        private MealPlan weeklyMealPlan = new MealPlan();

        public void Run()
        {
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": SetPreferences(); break;
                    case "2": LogMeal(); break;
                    case "3": GenerateMealPlan(); break;
                    case "4": GenerateGroceryList(); break;
                    case "5": Console.WriteLine("Goodbye!"); return;
                    default: Console.WriteLine("Invalid choice. Please select 1–5."); break;
                }
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Set Preferences");
            Console.WriteLine("2. Log a Meal");
            Console.WriteLine("3. Generate Weekly Meal Plan");
            Console.WriteLine("4. Generate Grocery List");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
        }

        private void SetPreferences()
        {
            Console.WriteLine("\nSetting Preferences:");
            preferences.TargetCalories = GetValidInt("Enter target daily calories: ");
            preferences.TargetProtein = GetValidInt("Enter target daily protein (g): ");
            preferences.TargetCarbs = GetValidInt("Enter target daily carbs (g): ");
            preferences.TargetFat = GetValidInt("Enter target daily fat (g): ");
            Console.WriteLine("Preferences set successfully!");
        }

        private void LogMeal()
        {
            Console.WriteLine("\nAvailable Meals:");
            for (int i = 0; i < Meal.AvailableMeals.Count; i++)
            {
                var m = Meal.AvailableMeals[i];
                Console.WriteLine($"{i + 1}. {m.Name} (Calories: {m.Calories}, Protein: {m.Protein}g, Carbs: {m.Carbs}g, Fat: {m.Fat}g)");
            }
            Console.Write("Enter the number of the meal to log: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= Meal.AvailableMeals.Count)
            {
                weeklyMealPlan.Meals.Add(Meal.AvailableMeals[index - 1]);
                Console.WriteLine("Meal logged successfully!");
            }
            else
            {
                Console.WriteLine("Invalid meal selection.");
            }
        }

        private void GenerateMealPlan()
        {
            if (!preferences.IsSet())
            {
                Console.WriteLine("Please set your preferences first.");
                return;
            }

            weeklyMealPlan.Generate(preferences);
        }

        private void GenerateGroceryList()
        {
            if (weeklyMealPlan.Meals.Count == 0)
            {
                Console.WriteLine("Please generate a meal plan first.");
                return;
            }

            var groceryList = weeklyMealPlan.GenerateGroceryList();
            Console.WriteLine("\nGrocery List:");
            foreach (var item in groceryList)
            {
                Console.WriteLine($"- {item.Key}: {item.Value} unit(s)");
            }
        }

        private int GetValidInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                    return value;
                Console.WriteLine("Please enter a valid positive number.");
            }
        }
    }
}