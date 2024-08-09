using System;

namespace WorkoutPlanner.Project
{
    /// <summary>
    /// Represents a user in the workout planner system, including their saved exercises
    /// and nutritional information.
    /// </summary>    
    public class User
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<Exercise> SavedExercises { get; private set; }
        public NutritionalInfo NutritionalInfo { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user. In real applications, store hashed passwords, not plain text.</param>
        public User(string username, string password)
        {
            Username = username;
            Password = password; // In real applications, store hashed passwords, not plain text
            SavedExercises = new List<Exercise>();
            NutritionalInfo = null;
        }

        /// <summary>
        /// Adds an exercise to the user's saved exercises and optionally prints a message.
        /// </summary>
        /// <param name="exercise">The exercise to be saved.</param>
        /// <param name="workout">A flag indicating whether the exercise is part of a workout.</param>
        public void SaveExercise(Exercise exercise, bool workout)
        {
            SavedExercises.Add(exercise);
            if (workout)
            {
                Console.WriteLine($"Exercise '{exercise.Workout}' saved to the database!");
            }
            else
            {
                Console.WriteLine($"Exercise '{exercise.Name}' saved to the database!");
            }
        }

        /// <summary>
        /// Displays the list of saved exercises to the console.
        /// </summary>
        public void DisplayUserData()
        {
            Console.WriteLine("Here is a list of your saved exercises: ");
            for (int i = 0; i < SavedExercises.Count; i++)
            {
                var ex = SavedExercises[i];
                if (string.IsNullOrEmpty(ex.Name))
                {
                    Console.WriteLine($"  {i + 1}) {ex.Workout}");
                }
                else
                {
                    Console.WriteLine($"  {i + 1}) {ex.Name}");
                }
            }
        }

        /// <summary>
        /// Gets the number of saved exercises for the user.
        /// </summary>
        /// <returns>The number of saved exercises.</returns>
        public int NumberOfExercises()
        {
            return SavedExercises.Count;
        }

        /// <summary>
        /// Retrieves an exercise from the list of saved exercises by index.
        /// </summary>
        /// <param name="index">The index of the exercise in the saved exercises list.</param>
        /// <returns>The exercise at the specified index.</returns>
        public Exercise GetExercise(int index)
        {
            return SavedExercises[index];
        }

        /// <summary>
        /// Saves the nutritional information for the user.
        /// </summary>
        /// <param name="info">The nutritional information to be saved.</param>
        public void SaveNutritionalInfo(NutritionalInfo info)
        {
            NutritionalInfo = info;
        }

        /// <summary>
        /// Checks if the user has nutritional information saved.
        /// </summary>
        /// <returns>True if nutritional information is saved; otherwise, false.</returns>
        public bool HasNutritionalInfo()
        {
            return NutritionalInfo != null;
        }

        /// <summary>
        /// Retrieves the user's nutritional information.
        /// </summary>
        /// <returns>The nutritional information of the user.</returns>
        public NutritionalInfo GetNutritionalInfo()
        {
            return NutritionalInfo;
        }
    }

}