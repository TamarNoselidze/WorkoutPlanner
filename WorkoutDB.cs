using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WorkoutPlanner.Project
{
    /// <summary>
    /// The <see cref="WorkoutDB"/> class handles fetching workout exercises
    /// from the Workout API.
    /// </summary>
    public class WorkoutDB
    {
        private static readonly string API_HOST = "work-out-api1.p.rapidapi.com";
        private static readonly string API_URL = "https://work-out-api1.p.rapidapi.com/search";
        private static readonly string API_KEY = "3345f16076msh89caec5bb418fc8p14bc11jsnbcff5c60c28f";

        /// <summary>
        /// Fetches workout exercises based on the provided parameters.
        /// </summary>
        /// <param name="muscles">The target muscle group for the workout.</param>
        /// <param name="equipment">The equipment used for the workout (optional).</param>
        /// <param name="intensityLevel">The intensity level of the workout (optional).</param>
        /// <returns>A list of <see cref="Exercise"/> objects containing the fetched workouts.</returns>
        public static List<Exercise> FetchWorkouts(string muscles, string? equipment, string? intensityLevel)
        {
            List<Exercise> exercises = new List<Exercise>();
            string queryUrl = $"{API_URL}?Muscles={muscles}";
            if (!string.IsNullOrEmpty(equipment))
            {
                queryUrl += $"&Equipment={equipment}";
            }
            if (!string.IsNullOrEmpty(intensityLevel))
            {
                queryUrl += $"&Intensity_Level={intensityLevel}";
            }

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

                        foreach (JToken element in jsonArray)
                        {
                            Exercise exercise = element.ToObject<Exercise>();
                            exercises.Add(exercise);
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

            return exercises;
        }
    }
}
