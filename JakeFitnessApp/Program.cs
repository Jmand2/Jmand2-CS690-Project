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
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        SetPreferences();
                        break;
                    case "2":
                        LogMeal();
                        break;
                    case "3":
                        GenerateMealPlan();
                        break;
                    case "4":
                        GenerateGroceryList();
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select 1-5.");
                        break;
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

        static void SetPreferences()
        {
            Console.WriteLine("\nSetting Preferences:");
            Console.Write("Enter target daily calories: ");
            preferences.TargetCalories = int.Parse(Console.ReadLine());
            Console.Write("Enter target daily protein (g): ");
            preferences.TargetProtein = int.Parse(Console.ReadLine());
            Console.Write("Enter target daily carbs (g): ");
            preferences.TargetCarbs = int.Parse(Console.ReadLine());
            Console.Write("Enter target daily fat (g): ");
            preferences.TargetFat = int.Parse(Console.ReadLine());
            Console.WriteLine("Preferences set successfully!");
        }

        static void LogMeal()
        {
            Console.WriteLine("\nAvailable Meals:");
            for (int i = 0; i < availableMeals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableMeals[i].Name} (Calories: {availableMeals[i].Calories}, " +
                    $"Protein: {availableMeals[i].Protein}g, Carbs: {availableMeals[i].Carbs}g, Fat: {availableMeals[i].Fat}g)");
            }
            Console.Write("Enter the number of the meal to log: ");
            int mealIndex = int.Parse(Console.ReadLine()) - 1;
            if (mealIndex >= 0 && mealIndex < availableMeals.Count)
            {
                loggedMeals.Add(availableMeals[mealIndex]);
                DisplayMealSummary();
            }
            else
            {
                Console.WriteLine("Invalid meal selection.");
            }
        }

        static void DisplayMealSummary()
        {
            int totalCalories = loggedMeals.Sum(m => m.Calories);
            int totalProtein = loggedMeals.Sum(m => m.Protein);
            int totalCarbs = loggedMeals.Sum(m => m.Carbs);
            int totalFat = loggedMeals.Sum(m => m.Fat);
            Console.WriteLine("\nLogged Meals Summary:");
            Console.WriteLine($"Total Calories: {totalCalories}");
            Console.WriteLine($"Total Protein: {totalProtein}g");
            Console.WriteLine($"Total Carbs: {totalCarbs}g");
            Console.WriteLine($"Total Fat: {totalFat}g");
        }

        static void GenerateMealPlan()
        {
            if (preferences.TargetCalories == 0)
            {
                Console.WriteLine("Please set preferences first.");
                return;
            }

            weeklyMealPlan.Meals.Clear();
            Random rand = new Random();
            int days = 7;
            int mealsPerDay = 3; // Breakfast, Lunch, Dinner
            int targetCaloriesPerMeal = preferences.TargetCalories / mealsPerDay;

            for (int day = 0; day < days; day++)
            {
                Console.WriteLine($"\nDay {day + 1}:");
                for (int meal = 0; meal < mealsPerDay; meal++)
                {
                    var suitableMeals = availableMeals
                        .Where(m => Math.Abs(m.Calories - targetCaloriesPerMeal) < 100)
                        .ToList();
                    if (suitableMeals.Count > 0)
                    {
                        var selectedMeal = suitableMeals[rand.Next(suitableMeals.Count)];
                        weeklyMealPlan.Meals.Add(selectedMeal);
                        Console.WriteLine($"- {selectedMeal.Name}");
                    }
                }
            }
            Console.WriteLine("Weekly meal plan generated!");
        }

        static void GenerateGroceryList()
        {
            if (weeklyMealPlan.Meals.Count == 0)
            {
                Console.WriteLine("Please generate a meal plan first.");
                return;
            }

            var groceryDict = new Dictionary<string, int>();
            foreach (var meal in weeklyMealPlan.Meals)
            {
                foreach (var ingredient in meal.Ingredients)
                {
                    groceryDict[ingredient] = groceryDict.ContainsKey(ingredient) ? groceryDict[ingredient] + 1 : 1;
                }
            }

            Console.WriteLine("\nGrocery List:");
            foreach (var item in groceryDict)
            {
                Console.WriteLine($"- {item.Key}: {item.Value} unit(s)");
            }
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