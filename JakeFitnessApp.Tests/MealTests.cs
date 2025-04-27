using Xunit;
using JakeFitnessApp;
using System.Collections.Generic;

namespace JakeFitnessApp.Tests
{
    public class MealTests
    {
        [Fact]
        public void Meal_Constructor_ShouldSetProperties()
        {
            var ingredients = new List<string> { "Oats", "Milk" };
            var meal = new Meal("Oatmeal", 300, 10, 50, 5, ingredients);

            Assert.Equal("Oatmeal", meal.Name);
            Assert.Equal(300, meal.Calories);
            Assert.Equal(10, meal.Protein);
            Assert.Equal(50, meal.Carbs);
            Assert.Equal(5, meal.Fat);
            Assert.Equal(ingredients, meal.Ingredients);
        }
    }
}