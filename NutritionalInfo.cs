using System;
using System.Collections.Generic;

namespace WorkoutPlanner.Project
{
    /// <summary>
    /// The <see cref="NutritionalInfo"/> class holds the nutritional information data.
    /// </summary>
    public class NutritionalInfo
    {
        public string MeasurementUnits { get; set; }
        public string Sex { get; set; }
        public int AgeValue { get; set; }
        public string AgeType { get; set; } = "yrs";
        public int Feet { get; set; }
        public int Inches { get; set; }
        public int Lbs { get; set; }
        public int Cm { get; set; }
        public int Kilos { get; set; }
        public string? ActivityLevel { get; set; }
        public string? Bmi { get; set; }
        public string? EstimatedCaloricNeeds { get; set; }
        public List<List<string>>? MacronutrientsTable { get; set; }
        public List<List<string>>? EssentialMineralsTable { get; set; }
        public List<List<string>>? NonEssentialMineralsTable { get; set; }
        public List<List<string>>? VitaminsTable { get; set; }

        /// <summary>
        /// Displays the nutritional information retrieved.
        /// </summary>
        public void DisplayNutritionalInfo()
        {
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Nutritional Information:");
            Console.WriteLine(" - BMI: " + Bmi);
            Console.WriteLine(" - Estimated Daily Caloric Needs: " + EstimatedCaloricNeeds);

    
            Console.WriteLine("\nMacronutrients:");
            foreach (var entry in MacronutrientsTable)
            {
                Console.WriteLine("  " + entry[0] + ": " + entry[1]);
            }

            Console.WriteLine("\nEssential Minerals:");
            foreach (var entry in EssentialMineralsTable)
            {
                Console.WriteLine("  " + entry[0] + ": " + entry[1] + " (UL: " + entry[2] + ")");
            }

            Console.WriteLine("\nNon-Essential Minerals:");
            foreach (var entry in NonEssentialMineralsTable)
            {
                Console.WriteLine("  " + entry[0] + ": " + entry[1] + " (UL: " + entry[2] + ")");
            }

            Console.WriteLine("\nVitamins:");
            foreach (var entry in VitaminsTable)
            {
                Console.WriteLine("  " + entry[0] + ": " + entry[1] + " (UL: " + entry[2] + ")");
            }
            Console.WriteLine("----------------------------------------------------------------------");
        }
    }
}
