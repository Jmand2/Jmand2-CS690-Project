using Xunit;
using JakeFitnessApp;
using System.Linq;

namespace JakeFitnessApp.Tests
{
    public class MealPlanTests
    {
        [Fact]
        public void Generate_ShouldAddMeals_WhenPreferencesSet()
        {
            var prefs = new UserPreferences
            {
                TargetCalories = 1800,
                TargetProtein = 100,
                TargetCarbs = 200,
                TargetFat = 70
            };

            var plan = new MealPlan();
            plan.Generate(prefs);

            Assert.True(plan.Meals.Count > 0);
        }

        [Fact]
        public void GenerateGroceryList_ShouldReturnDictionary()
        {
            var meal = new Meal("Test Meal", 300, 20, 40, 10, new List<string> { "Rice", "Chicken" });
            var plan = new MealPlan();
            plan.Meals.Add(meal);

            var groceryList = plan.GenerateGroceryList();

            Assert.Contains("Rice", groceryList.Keys);
            Assert.Contains("Chicken", groceryList.Keys);
            Assert.Equal(1, groceryList["Rice"]);
            Assert.Equal(1, groceryList["Chicken"]);
        }
    }
}