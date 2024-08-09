using System;
using Newtonsoft.Json.Linq;

namespace WorkoutPlanner.Project
{
    /// <summary>
    /// Provides methods for interacting with the ExerciseDB API to fetch exercise data.
    /// </summary>
    public class ExerciseDB
    {
        private static readonly string API_HOST = "exercisedb.p.rapidapi.com";
        private static readonly string API_KEY = "3345f16076msh89caec5bb418fc8p14bc11jsnbcff5c60c28f";
        private static readonly string API_URL = "https://exercisedb.p.rapidapi.com"; // Base URL for the ExerciseDB API

        /// <summary>
        /// Fetches a list of exercises from the ExerciseDB API based on the target body part.
        /// </summary>
        /// <param name="target">The target body part to filter exercises (e.g., "arms").</param>
        /// <param name="mappedTarget">The mapped target body part used in the API request (e.g., "upper-arm").</param>
        /// <returns>A list of <see cref="Exercise"/> objects that match the target criteria.</returns>
        public static List<Exercise> FetchExercises(string? target, string mappedTarget)
        {
            List<Exercise> exercises = new List<Exercise>();
            List<Exercise> filteredExercises = new List<Exercise>();
            string queryUrl = $"{API_URL}/exercises/bodyPart/{mappedTarget}"; // Adjust if using different endpoint

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", API_KEY);
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", API_HOST);

                    HttpResponseMessage response = client.GetAsync(queryUrl).Result; // Blocking call
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = response.Content.ReadAsStringAsync().Result; // Blocking call
                        JArray jsonArray = JArray.Parse(jsonString);

                        foreach (JToken token in jsonArray)
                        {
                            Exercise exercise = token.ToObject<Exercise>();
                            exercises.Add(exercise);
                        }

                        if (target.Equals(mappedTarget, StringComparison.OrdinalIgnoreCase))
                        {
                            filteredExercises = exercises;
                        }
                        else
                        {
                            filteredExercises = exercises.FindAll(ex => ex.Target.ToLower().Contains(target.ToLower()));
                        }
                    }
                    else
                    {
                        Console.WriteLine("GET request did not work");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return filteredExercises;
        }
    }
}
