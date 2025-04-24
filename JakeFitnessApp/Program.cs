using System;
using System.Collections.Generic;
using System.Linq;

namespace JakeFitnessApp
{
    class Program
    {
        static List<Meal> availableMeals = new List<Meal>
        {
            new Meal("Protein Shake", 200, 25, 10, 5, new List<string> { "Protein Powder", "Milk" }),
            new Meal("Chicken Salad", 400, 30, 20, 15, new List<string> { "Chicken Breast", "Lettuce", "Tomato" }),
            new Meal("Oatmeal", 300, 10, 50, 5, new List<string> { "Oats", "Milk" }),
            new Meal("Grilled Salmon", 500, 35, 10, 30, new List<string> { "Salmon", "Broccoli", "Olive Oil" })
        };

        static List<Meal> loggedMeals = new List<Meal>();
        static UserPreferences preferences = new UserPreferences();
        static MealPlan weeklyMealPlan = new MealPlan();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Jake's Fitness App!");
            Console.WriteLine("Tip: Set your preferences first to enable full features.\n");

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

        static void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Set Preferences");
            Console.WriteLine("2. Log a Meal");
            Console.WriteLine("3. Generate Weekly Meal Plan");
            Console.WriteLine("4. Generate Grocery List");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
        }

        static int GetValidInt(string prompt)
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

        static void SetPreferences()
        {
            Console.WriteLine("\nSetting Preferences:");
            preferences.TargetCalories = GetValidInt("Enter target daily calories: ");
            preferences.TargetProtein = GetValidInt("Enter target daily protein (g): ");
            preferences.TargetCarbs = GetValidInt("Enter target daily carbs (g): ");
            preferences.TargetFat = GetValidInt("Enter target daily fat (g): ");
            Console.WriteLine("Preferences set successfully!");
        }

        static void LogMeal()
        {
            Console.WriteLine("\nAvailable Meals:");
            for (int i = 0; i < availableMeals.Count; i++)
            {
                var m = availableMeals[i];
                Console.WriteLine($"{i + 1}. {m.Name} (Calories: {m.Calories}, Protein: {m.Protein}g, Carbs: {m.Carbs}g, Fat: {m.Fat}g)");
            }

            Console.Write("Enter the number of the meal to log: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= availableMeals.Count)
            {
                loggedMeals.Add(availableMeals[index - 1]);
                DisplayMealSummary();
            }
            else
            {
                Console.WriteLine("Invalid meal selection.");
            }
        }

        static void DisplayMealSummary()
        {
            Console.WriteLine("\nLogged Meals Summary:");
            Console.WriteLine($"Total Calories: {loggedMeals.Sum(m => m.Calories)}");
            Console.WriteLine($"Total Protein: {loggedMeals.Sum(m => m.Protein)}g");
            Console.WriteLine($"Total Carbs: {loggedMeals.Sum(m => m.Carbs)}g");
            Console.WriteLine($"Total Fat: {loggedMeals.Sum(m => m.Fat)}g");
        }

        static void GenerateMealPlan()
        {
            if (preferences.TargetCalories == 0)
            {
                Console.WriteLine("Please set your preferences before generating a meal plan.");
                return;
            }

            weeklyMealPlan.Meals.Clear();
            Random rand = new Random();
            int mealsPerDay = 3;
            int days = 7;
            int targetPerMeal = preferences.TargetCalories / mealsPerDay;

            for (int day = 0; day < days; day++)
            {
                Console.WriteLine($"\nDay {day + 1}:");
                for (int meal = 0; meal < mealsPerDay; meal++)
                {
                    var options = availableMeals.Where(m => Math.Abs(m.Calories - targetPerMeal) < 100).ToList();
                    if (options.Count > 0)
                    {
                        var chosen = options[rand.Next(options.Count)];
                        weeklyMealPlan.Meals.Add(chosen);
                        Console.WriteLine($"- {chosen.Name}");
                    }
                }
            }

            if (weeklyMealPlan.Meals.Count == 0)
                Console.WriteLine("No meals added. Please check preferences and try again.");
            else
                Console.WriteLine("Weekly meal plan generated successfully!");
        }

        static void GenerateGroceryList()
        {
            if (weeklyMealPlan.Meals.Count == 0)
            {
                Console.WriteLine("Please generate a meal plan before creating a grocery list.");
                return;
            }

            var groceryDict = new Dictionary<string, int>();
            foreach (var meal in weeklyMealPlan.Meals)
            {
                foreach (var item in meal.Ingredients)
                {
                    if (!groceryDict.ContainsKey(item))
                        groceryDict[item] = 0;
                    groceryDict[item]++;
                }
            }

            Console.WriteLine("\nGrocery List:");
            foreach (var item in groceryDict)
                Console.WriteLine($"- {item.Key}: {item.Value} unit(s)");
        }
    }

    class Meal
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
        public List<string> Ingredients { get; set; }

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

    class MealPlan
    {
        public List<Meal> Meals { get; set; } = new List<Meal>();
    }

    class UserPreferences
    {
        public int TargetCalories { get; set; }
        public int TargetProtein { get; set; }
        public int TargetCarbs { get; set; }
        public int TargetFat { get; set; }
    }
}
