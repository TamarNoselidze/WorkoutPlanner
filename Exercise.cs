using System;

namespace WorkoutPlanner.Project
{
    /// <summary>
    /// Represents an exercise with properties such as name, targeted body part, equipment, and instructions.
    /// </summary>
    public class Exercise
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? BodyPart { get; set; }
        public string? Equipment { get; set; }
        public string? Target { get; set; }
        public string? GifUrl { get; set; }
        public List<string>? SecondaryMuscles { get; set; }
        public List<string>? Instructions { get; set; }

        public string? Muscle { get; set; }
        public string? Workout { get; set; }
        public string? IntensityLevel { get; set; }
        public string? BeginnerSets { get; set; }
        public string? IntermediateSets { get; set; }
        public string? ExpertSets { get; set; }
        public string? Explanation { get; set; }
        public string? LongExplanation { get; set; }
        public string? Video { get; set; }

        private static List<string> muscleList = new List<string>
        {
            "biceps", "triceps", "chest", "abs", "back", "legs", "lats", "hamstring", "calves", "quadriceps", "trapezius", "shoulders", "glutes"
        };

        private static List<string> equipmentList = new List<string>
        {
            "barbell", "dumbbells", "kettlebell", "bench", "incline bench", "decline bench", "cable machine", "resistance band", "bicep curl machine", "leg press"
        };

        private static readonly Dictionary<string, string> bodyPartMap = new Dictionary<string, string>
        {
            { "back", "back" },
            { "cardio", "cardio" },
            { "chest", "chest" },
            { "lower arms", "lower arms" },
            { "lower legs", "lower legs" },
            { "neck", "neck" },
            { "shoulders", "shoulders" },
            { "upper arms", "upper arms" },
            { "upper legs", "upper legs" },
            { "waist", "waist" },
            { "biceps", "upper arms" },
            { "triceps", "upper arms" },
            { "quads", "upper legs" },
            { "hamstrings", "upper legs" },
            { "glutes", "upper legs" },
            { "calves", "lower legs" },
            { "shins", "lower legs" },
            { "abs", "waist" }
        };

        /// <summary>
        /// Retrieves the instructions for performing the exercise as a formatted string.
        /// </summary>
        /// <returns>A string containing the exercise instructions.</returns>
        public string GetInstructions()
        {
            return string.Join("\n", Instructions);
        }

        /// <summary>
        /// Displays general information about the exercise.
        /// </summary>
        public void DisplayExerciseInfoGeneral()
        {
            Console.WriteLine($"\nInformation about >{Name}<:");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"This exercise mainly targets {BodyPart}.");
            Console.Write(" - Also, the secondary muscles are: ");
            for (int i = 0; i < SecondaryMuscles.Count; i++)
            {
                if (i == SecondaryMuscles.Count - 1)
                {
                    Console.WriteLine($"and {SecondaryMuscles[i]}.");
                }
                else
                {
                    Console.Write($"{SecondaryMuscles[i]}, ");
                }
            }

            Console.WriteLine($"\nNecessary equipment: {Equipment}.");
            Console.WriteLine("\nInstructions: ");
            foreach (var instruction in Instructions)
            {
                Console.WriteLine($"  * {instruction}");
            }

            Console.WriteLine("\nFollow the link to see a visual demonstration of the exercise: ");
            Console.WriteLine(GifUrl);
            Console.WriteLine("-----------------------------------------------------------");
        }

        /// <summary>
        /// Displays detailed information about the workout, including the muscle targeted, set/rep ranges, and instructions.
        /// </summary>
        public void DisplayWorkoutInfo()
        {
            Console.WriteLine($"\nInformation about >{Workout}<:");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"This exercise mainly targets {Muscle}.");

            if (string.IsNullOrEmpty(IntensityLevel))
            {
                Console.WriteLine($"- Set/Rep range for beginners: {BeginnerSets}");
                Console.WriteLine($"- Set/Rep range for intermediate level: {IntermediateSets}");
                Console.WriteLine($"- Set/Rep range for expert level: {ExpertSets}");
            }
            else
            {
                switch (IntensityLevel)
                {
                    case "beginner":
                        Console.WriteLine($"- Set/Rep range: {BeginnerSets}");
                        break;
                    case "intermediate":
                        Console.WriteLine($"- Set/Rep range: {IntermediateSets}");
                        break;
                    default:
                        Console.WriteLine($"- Set/Rep range: {ExpertSets}");
                        break;
                }
            }

            Console.WriteLine("\nInstructions: ");
            Console.WriteLine($"  > The equipment you will need for this workout: {Equipment}");

            var instructionsSource = !string.IsNullOrEmpty(LongExplanation) ? LongExplanation : Explanation;
            if (!string.IsNullOrEmpty(instructionsSource))
            {
                var instructionsList = instructionsSource.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(instr => instr.Trim())
                    .Where(instr => !string.IsNullOrEmpty(instr))
                    .ToList();

                foreach (var instruction in instructionsList)
                {
                    Console.WriteLine($"  * {instruction}");
                }
            }
            else
            {
                Console.WriteLine("  * No detailed instructions available.");
            }

            Console.WriteLine("\nFollow the link to see a video on YouTube demonstrating the exercise: ");
            Console.WriteLine(Video);
            Console.WriteLine("-----------------------------------------------------------");
        }

        /// <summary>
        /// Displays a list of available target muscles.
        /// </summary>
        public static void DisplayMuscleList()
        {
            Console.WriteLine("\nPlease, choose a target muscle from the list below: ");
            foreach (var muscle in muscleList)
            {
                Console.WriteLine($" * {muscle};");
            }

            Console.WriteLine("You can also request for 'stretching' or 'warm-up' exercises. \n");
        }

        /// <summary>
        /// Displays a list of available equipment.
        /// </summary>
        public static void DisplayEquipmentList()
        {
            Console.WriteLine("\n Please, choose an equipment from the list below: ");
            foreach (var eq in equipmentList)
            {
                Console.WriteLine($" * {eq};");
            }
            Console.WriteLine("   . . .\n");
        }

        /// <summary>
        /// Validates if the given target muscle is in the list of available muscles.
        /// </summary>
        /// <param name="targetMuscle">The target muscle to validate.</param>
        /// <returns>True if the target muscle is valid; otherwise, false.</returns>
        public static bool IsTargetValid(string? targetMuscle)
        {
            if (string.IsNullOrEmpty(targetMuscle)){
                return false;
            }
            return muscleList.Contains(targetMuscle);
        }

        /// <summary>
        /// Validates if the given equipment is in the list of available equipment.
        /// </summary>
        /// <param name="equipment">The equipment to validate.</param>
        /// <returns>True if the equipment is valid; otherwise, false.</returns>
        public static bool IsEquipmentValid(string? equipment)
        {
            if (string.IsNullOrEmpty(equipment)){
                return false;
            }
            return equipmentList.Contains(equipment);
        }

        /// <summary>
        /// Maps a user-specified body part to the corresponding category used by the application.
        /// </summary>
        /// <param name="userBodyPart">The body part specified by the user.</param>
        /// <returns>The corresponding body part category if found; otherwise, null.</returns>
        public static string GetMappedBodyPart(string? userBodyPart)
        {
            return bodyPartMap.TryGetValue(userBodyPart.ToLower(), out var apiCategory) ? apiCategory : null;
        }
    }
}
