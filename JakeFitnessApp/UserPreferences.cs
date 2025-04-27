namespace JakeFitnessApp
{
    public class UserPreferences
    {
        public int TargetCalories { get; set; }
        public int TargetProtein { get; set; }
        public int TargetCarbs { get; set; }
        public int TargetFat { get; set; }

        public bool IsSet()
        {
            return TargetCalories > 0;
        }
    }
}
