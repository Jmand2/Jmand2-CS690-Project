using Xunit;
using JakeFitnessApp;

namespace JakeFitnessApp.Tests
{
    public class UserPreferencesTests
    {
        [Fact]
        public void IsSet_ShouldReturnTrue_WhenCaloriesPositive()
        {
            var prefs = new UserPreferences { TargetCalories = 2000 };
            Assert.True(prefs.IsSet());
        }

        [Fact]
        public void IsSet_ShouldReturnFalse_WhenCaloriesZero()
        {
            var prefs = new UserPreferences { TargetCalories = 0 };
            Assert.False(prefs.IsSet());
        }
    }
}