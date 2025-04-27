using System;

namespace JakeFitnessApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Jake's Fitness App!");
            Console.WriteLine("Tip: Set your preferences first to generate plans correctly.\n");

            var app = new AppController();
            app.Run();
        }
    }
}