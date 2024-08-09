using System;
using Newtonsoft.Json.Linq;

namespace WorkoutPlanner.Project
{
    /// <summary>
    /// The <see cref="NutritionalDB"/> class handles fetching nutritional information
    /// from the Nutrition Calculator API.
    /// </summary>
    public class NutritionalDB
    {
        private static readonly string API_HOST = "nutrition-calculator.p.rapidapi.com";
        private static readonly string API_URL = "https://nutrition-calculator.p.rapidapi.com/api/nutrition-info";
        private static readonly string API_KEY = "3345f16076msh89caec5bb418fc8p14bc11jsnbcff5c60c28f";

        /// <summary>
        /// Fetches nutritional information based on the provided parameters.
        /// </summary>
        /// <param name="measurementUnits">The measurement units, either "std" (standard) or "met" (metric).</param>
        /// <param name="sex">The sex of the user, either "male" or "female".</param>
        /// <param name="ageValue">The age of the user in years.</param>
        /// <param name="feet">The height in feet (used if <paramref name="measurementUnits"/> is "std").</param>
        /// <param name="inches">The height in inches (used if <paramref name="measurementUnits"/> is "std").</param>
        /// <param name="lbs">The weight in pounds (used if <paramref name="measurementUnits"/> is "std").</param>
        /// <param name="cm">The height in centimeters (used if <paramref name="measurementUnits"/> is "met").</param>
        /// <param name="kilos">The weight in kilograms (used if <paramref name="measurementUnits"/> is "met").</param>
        /// <param name="activityLevel">The activity level of the user.</param>
        /// <returns>A <see cref="NutritionalInfo"/> object containing the fetched nutritional information.</returns>
        public static NutritionalInfo FetchNutritionalInfo(
            string measurementUnits, string sex, int ageValue, int feet, int inches, int lbs, 
            int cm, int kilos, string activityLevel)
        {
            NutritionalInfo nutritionalInfo = new NutritionalInfo();
            string queryUrl = $"{API_URL}?measurement_units={measurementUnits}&sex={sex}&age_value={ageValue}&age_type=yrs";
            
            if (feet != 0)
            {
                queryUrl += $"&feet={feet}&inches={inches}&lbs={lbs}&activity_level={activityLevel}";
            }
            else
            {
                queryUrl += $"&cm={cm}&kilos={kilos}&activity_level={activityLevel}";
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
                        JObject jsonObject = JObject.Parse(jsonString);

                        nutritionalInfo.MeasurementUnits = measurementUnits;
                        nutritionalInfo.Sex = sex;
                        nutritionalInfo.AgeValue = ageValue;

                        if (feet != 0)
                        {
                            nutritionalInfo.Feet = feet;
                            nutritionalInfo.Inches = inches;
                            nutritionalInfo.Lbs = lbs;
                        }
                        else
                        {
                            nutritionalInfo.Cm = cm;
                            nutritionalInfo.Kilos = kilos;
                        }

                        nutritionalInfo.ActivityLevel = activityLevel;
                        nutritionalInfo.Bmi = jsonObject["BMI_EER"]["BMI"].ToString();
                        nutritionalInfo.EstimatedCaloricNeeds = jsonObject["BMI_EER"]["Estimated Daily Caloric Needs"].ToString();

                        // Parse macronutrients_table
                        JArray macronutrients = (JArray)jsonObject["macronutrients_table"]["macronutrients-table"];
                        nutritionalInfo.MacronutrientsTable = macronutrients.ToObject<List<List<string>>>();

                        // Parse essential-minerals-table
                        JArray essentialMinerals = (JArray)jsonObject["minerals_table"]["essential-minerals-table"];
                        nutritionalInfo.EssentialMineralsTable = essentialMinerals.ToObject<List<List<string>>>();

                        // Parse non-essential-minerals-table
                        JArray nonEssentialMinerals = (JArray)jsonObject["non_essential_minerals_table"]["non-essential-minerals-table"];
                        nutritionalInfo.NonEssentialMineralsTable = nonEssentialMinerals.ToObject<List<List<string>>>();

                        // Parse vitamins-table
                        JArray vitamins = (JArray)jsonObject["vitamins_table"]["vitamins-table"];
                        nutritionalInfo.VitaminsTable = vitamins.ToObject<List<List<string>>>();
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

            return nutritionalInfo;
        }
    }
}
